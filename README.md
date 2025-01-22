# TenStar - UserApplication

## Setup database

```powershell

# needed for migration
dotnet tool install --global dotnet-ef && dotnet tool update --global dotnet-ef

# working directory repository root, if windows ensure docker desktop is running, -d detatched
docker-compose -f ".\scripts\docker.compose.sqlserver.yml" up -d

# migration
cd ./src/TenStar.App/
dotnet-ef migrations add init
dotnet-ef database update
cd ..\..

# init the powershell command aliases
. .\init.ps1

# run the full app
run-stack

# more commands
sql-up
sql-down
```

## Experimental - Full docker in db, web, webapi

```shell
# Backend baseurl bug in User.Web
# Experimental, use the root docker-compose.yml
# Bug 
docker-compose up
```
