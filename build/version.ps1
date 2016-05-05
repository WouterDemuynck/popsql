$commitId = git rev-parse --short HEAD
Write-Host "##vso[task.setvariable variable=commitId;]$commitId"