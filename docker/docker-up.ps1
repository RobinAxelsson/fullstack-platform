#!/usr/bin/env pwsh
# dependent on . .\init.ps1

function wait-for-tcp($port){
    for ($i = 0; $i -lt 3; $i++) {
        $result = Test-Connection localhost -TCPPort $port -Count 1
        if($result) {
            break;
        }
    }
}

if($args.Count -eq 0){
    Write-Host "Usage: docker-up [reverse_proxy|database|all] --update-db --sync-nginx"
    exit 1
}

docker network create app_network

if($args[0] -eq "reverse_proxy" -or $args[0] -eq "all"){
    $proxy = Join-Path $PSScriptRoot docker-compose.proxy.yml
    docker-compose -f $proxy up -d

    if($args -contains "--sync-nginx"){
        wait-for-tcp 8888
        Write-Host "$ Syncing nginx reverse proxy..."
        sync-nginx
    }
}

if($args[0] -eq "database" -or $args[0] -eq "all"){
    $database = Join-Path $PSScriptRoot docker-compose.database.yml
    docker-compose -f $database up -d

    if($args -contains "--update-db"){
        Write-Host "$ Updating database..."
        wait-for-tcp 1433
        Sleep 2
        ef-update
    }
}

docker ps