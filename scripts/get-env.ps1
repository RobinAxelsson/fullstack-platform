#!/usr/bin/env pwsh

param (
    [string]$envFile,
    [string]$key
)

function helpText(){
    Write-Host "Usage: get-env <env-file> <key>"
}

$isHelp = $action -eq "help" -or $action -eq "h" -or $action -eq "-h" -or $action -eq "--help"
if($args -lt 2 -or $isHelp) {
    helpText
    exit 1
}

if($envFile -notmatch "\.env") {
    Write-Host "The file $envFile is not a .env file."
    exit 1
}

if(-not (Test-Path $envFile)) {
    $scriptsEnv = Join-Path -Path (Resolve-Path "$PSScriptRoot\..") -ChildPath "scripts-env"
    $envFile = Join-Path -Path $scriptsEnv -ChildPath $envFile
}

if(-not (Test-Path $envFile)) {
    Write-Host "The file $envFile does not exist."
    exit 1
}

$envVars = Get-Content $envFile

if(-not $envVars) {
    Write-Host "The file $envFile is empty."
    exit 1
}

foreach($envVar in $envVars) {
    $keyValue = $envVar.Split("=")
    $key = $keyValue[0]
    $value = $keyValue[1]
    if($key -eq $key) {
        Write-Output $value
        exit 0
    }
}

Write-Host "The key $key does not exist in the file $envFile."
help-text
exit 1