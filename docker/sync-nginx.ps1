#!/usr/bin/env pwsh

# get the docker host url for either windlows or linux eg http://host.docker.internal
$host_docker = "http://host.docker.internal" #TODO: implement for linux

$config = Join-Path $PSScriptRoot nginx.conf
$docker_config = "/etc/nginx/nginx.conf"

$proxy_port = 8888
$backend_url = "${host_docker}:5157/api"
$frontend_url = "${host_docker}:5147"

$config_temp = "${config}.temp"
cat $config | ForEach-Object { $_ -replace '_PROXY_PORT_', $proxy_port } | ForEach-Object { $_ -replace '_BACKEND_URL_', $backend_url } | ForEach-Object { $_ -replace '_FRONTEND_URL_', $frontend_url } > $config_temp
docker cp $config_temp reverse_proxy:$docker_config
docker exec reverse_proxy nginx -s reload
rm $config_temp