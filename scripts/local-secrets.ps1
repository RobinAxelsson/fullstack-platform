#!/usr/bin/env pwsh

param (
    [string]$action,
    [string]$key
)

$isHelp = $action -eq "help" -or $action -eq "h" -or $action -eq "-h" -or $action -eq "--help"
$actions = @("edit", "add", "get", "list")

if($actions -notcontains $action -or $isHelp) {
    Write-Host "local-secrets: manage local secrets and storing them in <USERPROFILE>."
    Write-Host "Usage: local-secrets [action] [key]"
    Write-Host "Actions:"
    Write-Host "  edit: Edit local secrets file"
    Write-Host "  add: Add new key-value pair to local secrets file"
    Write-Host "  get: Get the value of a key from the local secrets file"
    Write-Host "  list: Print the contents of the local secrets file"

    exit 1
}

$repo_name = $Env:_REPO_NAME_

if(-not $repo_name) {
    Write-Host "The repository name was not found while trying to create the local-secrets folder."
    Write-Host "Please make sure the _REPO_NAME_ environment variable is set and the script environment is loaded with . .\init.ps1."
    exit 1
}

$localSecretsDir = "$env:USERPROFILE\${repo_name}"
$localSecrets = "${localSecretsDir}\local-secrets.env"

if(-not (Test-Path "$localSecretsDir")) {
    New-Item -ItemType Directory -Path "$localSecretsDir"
}

if(-not (Test-Path "$localSecrets")) {
    New-Item -ItemType File -Path "$localSecrets"
}

if($action -eq "edit") {
    Start-Process "$localSecrets"
}

if($action -eq "add") {
    $key = Read-Host "Enter key"
    $value = Read-Host "Enter value"
    Add-Content $localSecrets "$key=$value"
}

if($action -eq "get") {
    $envVars = Get-Content $localSecrets
    foreach($envVar in $envVars) {
        $keyValue = $envVar.Split("=")
        if($keyValue[0] -eq $key) {
            Write-Host $keyValue[1]
            exit 0
        }
    }
    Write-Host "Environment variable '$key' does not exist in local-secrets.env."
    exit 1
}

if($action -eq "list") {
    if(-not (Get-Content $localSecrets)) {
        Write-Host "The file $localSecrets is empty."
        exit 0
    }
    Get-Content $localSecrets | Write-Host
}