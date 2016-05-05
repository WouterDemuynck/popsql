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

$commitId = git rev-parse --short HEAD
$majorVersion = $Env:MajorVersion
$minorVersion = $Env:MinorVersion
$patchVersion = $Env:PatchVersion
$prereleaseVersion = $Env:PrereleaseVersion

$packageVersion = "$majorVersion.$minorVersion.$patchVersion"
if (-not $prereleaseVersion -eq "")
{
	$packageVersion = "$packageVersion-$prereleaseVersion-$commitId"
}

Write-Host "##vso[task.setvariable variable=commitId;]$commitId"
Write-Host "##vso[task.setvariable variable=packageVersion;]$packageVersion"

# Update AssemblyInfo files.
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

$build = Get-ScriptDirectory
$buildTools = Join-Path $build 'BuildTools.dll'
Add-Type -Path $buildTools


$assemblyVersion = "$majorVersion.$minorVersion.$patchVersion"
$assemblyFileVersion = [BuildTools.VersionNumberGenerator]::GenerateVersion(3, 0, [BuildTools.BuildNumberType]::YearMonthDay, [BuildTools.RevisionNumberType]::HourMinute, $(Get-Date -Date "2012-09-29 00:00:00Z")).ToString()
$assemblyInfoVersion = "$packageVersion"

foreach ($file in (Get-ChildItem -path $src -include 'AssemblyInfo.cs' -recurse))
{
	Write-Host "Updating  '$($o.FullName)' -> $Version"
	
	$assemblyVersionPattern = '(AssemblyVersion\()("[^"]*")(\))'
	$fileVersionPattern = '(AssemblyFileVersion\()("[^"]*")(\))'
	$assemblyInfoPattern = '(AssemblyInformationalVersion\()("[^"]*")(\))'
	
	(Get-Content $file.FullName) -replace $assemblyVersionPattern, "`$1`"$assemblyVersion`"`$3" `
								 -replace $fileVersionPattern, "`$1`"$assemblyFileVersion`"`$3" `
								 -replace $assemblyInfoPattern, "`$1`"$assemblyInfoVersion`"`$3" `
		| Out-File $file.FullName -encoding UTF8 -force
}
