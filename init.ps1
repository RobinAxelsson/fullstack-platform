Set-Alias run-web .\scripts\run-web.bat
Set-Alias clean-web .\scripts\clean-web.bat
Set-Alias watch-web .\scripts\watch-web.bat
Set-Alias run-webapi .\scripts\run-webapi.bat
Set-Alias watch-webapi .\scripts\watch-webapi.bat
function webapi(){ docker exec TenStar.UserWeb curl -v http://TenStar.UserWebApi:8088/api/weatherforecast }
function apiweb(){ docker exec TenStar.UserWebApi curl -v http://TenStar.UserWeb }
function apihost(){ docker exec TenStar.UserWebApi curl -v http://host.docker.internal:8088/api/weatherforecast }
function webhost(){ docker exec TenStar.UserWeb curl -v http://host.docker.internal }
function rsweb(){ docker exec -it TenStar.UserWeb sh }
function rsapi(){ docker exec -it TenStar.UserWeb sh }