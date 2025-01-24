#!/usr/bin/env pwsh

function helpText(){
    Write-Host "op is the 1Password CLI tool, and this script runs a <command-expression> wrapped inside a 1Password secrets envrionment."
    Write-Host "All the environment variables are stored in the file op.env with references to each key in the 1Password Vault."
    Write-Host "Usage: op-run <command-expression>"
    Write-Host "Usage: op-run dotnet-x run TenStar.UserWeb"
}

$isHelp = $action -eq "help" -or $action -eq "h" -or $action -eq "-h" -or $action -eq "--help"
if($args -lt 1 -or $isHelp) {
    helpText
    exit 1
}

$root = Join-Path -Path (Resolve-Path "$PSScriptRoot\..")
$envFile = Join-Path -Path $root -ChildPath "op.env"

if(-not (Test-Path $envFile)) {
    Write-Host "The file $envFile does not exist."
    exit 1
}

$envVars = Get-Content $envFile

if(-not $envVars) {
    Write-Host "The file $envFile is empty."
    exit 1
}

$expression = "op run --env-file=$envFile -- pwsh -c $args"
Invoke-Expression $expression