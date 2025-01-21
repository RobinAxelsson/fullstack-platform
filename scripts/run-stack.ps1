function Start-Project($project){
    $projectPath = ".\src\$($project)\$($project).csproj"
    if (-Not (Test-Path $projectPath)) {
        Write-Host "Error: Project file '$projectPath' does not exist." -ForegroundColor Red
        return
    }

    # cmd terminal
    Start-Process cmd -ArgumentList "/k dotnet run --project $projectpath"
    
    # powershell terminal
    #Start-Process powershell -ArgumentList "-NoExit", "-Command", "dotnet run --project $projectpath"
}

Start-Project("TenStar.UserWebApi")
Start-Project("TenStar.UserWeb")