# TenStar - UserApplication

Fullstack platform that started out as a code interview test project.

## Requirements

- Dotnet SDK 9
- Docker (or SQL-Server)
- Powershell (or an IDE)

## Setup Docker SQL database

If you prefer setting up sql server directly on your host machine and skipping docker it should work as well (not tested). The steps below should be followed anyway except for the docker-compose part.

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

## Http testing

Use postman and newman for pipeline testing

## Local Secrets

- Use "local-secrets" script
- USERPROFILE/tenstar/local-secrets.env

## Secret Secrets 1Password

- Use "secret-secrets" script requires 1Password desktop, 1password account and 1Password CLI https://developer.1password.com/docs/cli/get-started/#install
- Alternatives in pipelines GitHub secrets

```shell
winget install 1password-cli
```
