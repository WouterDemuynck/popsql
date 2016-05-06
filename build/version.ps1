if(-not $Env:MajorVersion)
{
    Write-Host "You must set the following environment variables to test this script interactively:"
    Write-Host '$Env:MajorVersion = 3'
    Write-Host '$Env:MinorVersion = 0'
    Write-Host '$Env:PatchVersion = 1'
	Write-Host
	Write-Host "Optionally you can set a pre-release version too:"
    Write-Host '$Env:PrereleaseVersion = "alpha"'
	Write-Host
	Write-Error "Required environment variables not found."
    exit 1
}

function Get-ScriptDirectory 
{
	Split-Path -parent $PSCommandPath
}

$src = $Env:BUILD_SOURCESDIRECTORY
if (-not $src)
{
	$src = Get-ScriptDirectory
	$src = "$src\..\src\"
}

# Get current commit hash.
$commitId = git rev-parse --short HEAD
$majorVersion = $Env:MajorVersion
$minorVersion = $Env:MinorVersion
$patchVersion = $Env:PatchVersion
$prereleaseVersion = $Env:PrereleaseVersion

# Auto-generate version numbers.
$build = Get-ScriptDirectory
$buildTools = Join-Path $build 'BuildTools.dll'
Add-Type -Path $buildTools
$generatedVersion = [BuildTools.VersionNumberGenerator]::GenerateVersion($majorVersion, $minorVersion, [BuildTools.BuildNumberType]::YearMonthDay, [BuildTools.RevisionNumberType]::HourMinute, $(Get-Date -Date "2012-09-29 00:00:00Z"))

$packageVersion = "$majorVersion.$minorVersion.$patchVersion"
if (-not $prereleaseVersion -eq "")
{
	$packageVersion = "$packageVersion-$prereleaseVersion-$($generatedVersion.Build)-$($generatedVersion.Revision)"
}

$assemblyVersion = "$majorVersion.$minorVersion.$patchVersion"
$assemblyFileVersion = $generatedVersion.ToString()
$assemblyInfoVersion = "$packageVersion"
$assemblyTitle = "Popsql $assemblyVersion (rev. $commitId)"
$assemblyCopyright = "Copyright $([char]0x00A9) 2012-$([DateTime]::Now.Year) Wouter Demuynck"

Write-Host "Assembly Title: $assemblyTitle"
Write-Host "Assembly Copyright: $assemblyCopyright"
Write-Host "Assembly Version: $assemblyVersion"
Write-Host "Assembly File Version: $assemblyFileVersion"
Write-Host "Assembly Informational Version: $assemblyInfoVersion"

# Publish variables to VSTS.
Write-Host "##vso[task.setvariable variable=packageVersion;]$packageVersion"
Write-Host "##vso[task.setvariable variable=commitId;]$commitId"

# Update assembly information.
foreach ($file in (Get-ChildItem -path $src -include 'AssemblyInfo.cs' -recurse))
{	
	$assemblyTitlePattern = '(AssemblyTitle\()("[^"]*")(\))'
	$assemblyCopyrightPattern = '(AssemblyCopyright\()("[^"]*")(\))'
	$assemblyVersionPattern = '(AssemblyVersion\()("[^"]*")(\))'
	$fileVersionPattern = '(AssemblyFileVersion\()("[^"]*")(\))'
	$assemblyInfoPattern = '(AssemblyInformationalVersion\()("[^"]*")(\))'
	
	(Get-Content $file.FullName) -replace $assemblyTitlePattern, "`$1`"$assemblyTitle`"`$3" `
								 -replace $assemblyCopyrightPattern, "`$1`"$assemblyCopyright`"`$3" `
								 -replace $assemblyVersionPattern, "`$1`"$assemblyVersion`"`$3" `
								 -replace $fileVersionPattern, "`$1`"$assemblyFileVersion`"`$3" `
								 -replace $assemblyInfoPattern, "`$1`"$assemblyInfoVersion`"`$3" `
		| Out-File $file.FullName -encoding Unicode -force
}
