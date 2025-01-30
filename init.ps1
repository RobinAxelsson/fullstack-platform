#!/usr/bin/env pwsh

function AddDirToPath($dir){
    $fullDir = Join-Path $PSScriptRoot $dir

    if (-not ($env:Path -split $pathSep -contains $fullDir)) {
        $env:Path += [System.IO.Path]::PathSeparator + $fullDir
    }
}

AddDirToPath "scripts"
AddDirToPath "docker"

$env:_REPO_NAME_ = "TenStar"
$env:_ROOT_PATH_ = $PSScriptRoot
$backendName = "${env:_REPO_NAME_}.UserWebApi"
$frontendName = "${env:_REPO_NAME_}.UserWeb"
$efProjName = "${env:_REPO_NAME_}.UserContext"

$env:_EF_PROJ_PATH_ = Join-Path $PSScriptRoot "src" "$efProjName"

$env:_WEB_API_PATH_ = Join-Path $PSScriptRoot "src" $backendName "${backendName}.csproj"
$env:_WEB_PATH_ = Join-Path $PSScriptRoot "src" "$frontendName" "${frontendName}.csproj"