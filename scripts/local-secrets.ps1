param (
    [string]$action,
    [string]$key
)

$isHelp = $action -eq "help" -or $action -eq "h" -or $action -eq "-h" -or $action -eq "--help"

if(-not $action -or $isHelp) {
    Write-Host "Local Secrets is not for production use. It is intended for local development only."
    Write-Host "Usage: local-secrets [action] [key]"
    Write-Host "Actions:"
    Write-Host "  edit: Edit local secrets file"
    Write-Host "  add: Add new key-value pair to local secrets file"
    Write-Host "  env-activate: Activate local secrets as environment variables"
    Write-Host "  exists: Check if an environment variable exists"
    Write-Host "  list: Print the contents of the local secrets file"
    
    if($isHelp) {
        exit 0
    }

    exit 1
}

$localSecrets = "$env:USERPROFILE\tenstar\local-secrets.env"

if(-not (Test-Path "$env:USERPROFILE\tenstar")) {
    New-Item -ItemType Directory -Path "$env:USERPROFILE\tenstar"
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

if($action -eq "env-activate" -and -not $key) {
    $envVars = Get-Content $localSecrets
    Test-EnvFileContent $envVars

    foreach($envVar in $envVars) {
        $keyValue = $envVar.Split("=")
        $key = $keyValue[0]
        $value = $keyValue[1]
        [System.Environment]::SetEnvironmentVariable($key, $value, [System.EnvironmentVariableTarget]::User)
    }
}

if($action -eq "exists") {
    if(-not $key) {
        Write-Host "Please provide a key to check."
        exit 1
    }

    $envVars = Get-Content $localSecrets
    foreach($envVar in $envVars) {
        $keyValue = $envVar.Split("=")
        if($keyValue[0] -eq $key) {
            Write-Host "Environment variable '$key' exists."
            exit 0
        }
    }

    Write-Host "Environment variable '$key' does not exist."
    exit 1
}

if($action -eq "list") {
    Get-Content $localSecrets | ForEach-Object { Write-Host $_ }
}