#!/usr/bin/env pwsh

Start-Process pwsh -ArgumentList '-NoExit', '-c', 'dotnet run --project $env:_WEB_PATH_' 
