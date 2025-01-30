#!/usr/bin/env pwsh

$db = Join-Path $PSScriptRoot docker-compose.database.yml
$proxy = Join-Path $PSScriptRoot docker-compose.proxy.yml
docker-compose -f $db -f $proxy down
