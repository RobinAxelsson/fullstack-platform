#!/usr/bin/env pwsh

if ($args.Length -lt 2) {
    Write-Host "Usage: dotnet-x <dotnet command> <project>"
    Write-Host "Example: dotnet-x run --no-restore TenStar.UserWeb"
    exit 1
}

$command = $args[0..($args.Length - 2)]
$project = $args[-1]

if($project -notmatch "\.csproj") {
    $project = "$project.csproj"
}

$projectFile = Get-ChildItem -Path "." -Recurse -Filter $project | Select-Object -First 1

if(-not $projectFile) {
    Write-Host "The project $project does not exist."
    exit 1
}

$path = $projectFile.FullName

$dotnetCommand = "dotnet $command $($options -join ' ') --project $($path -join ' ')"
Invoke-Expression $dotnetCommand