#!/usr/bin/env pwsh

$config = Join-Path $PSScriptRoot nginx.conf
docker cp $config reverse_proxy:/etc/nginx/nginx.conf
docker exec reverse_proxy nginx -s reload