#!/usr/bin/env pwsh

param (
    [string]$arg0
)

if($arg0 -eq "list"){
    docker ps
    exit 0
}

$dockerContainer = $arg0
Test $dockerContainer {
    Write-Host "Usage: docker-remote <docker-container>"
    exit 1
}

docker exec -it $dockerContainer sh