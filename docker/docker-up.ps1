#!/usr/bin/env pwsh

$dockerCompose = Join-Path $PSScriptRoot docker-compose.infra.yml
docker-compose -f $dockerCompose up -d