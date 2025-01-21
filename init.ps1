Set-Alias run-stack .\scripts\run-stack.ps1
Set-Alias run-web .\scripts\run-web.ps1
Set-Alias watch-web .\scripts\watch-web.ps1
Set-Alias run-webapi .\scripts\run-webapi.ps1
Set-Alias watch-webapi .\scripts\watch-webapi.ps1

### experimental ###

# remote shell into the containers
function remote-web(){ docker exec -it TenStar.UserWeb sh }
function remote-api(){ docker exec -it TenStar.UserWeb sh }
function remote-db(){ docker exec -it TenStar.Db sh }

# configure nginx web server in TenStar.UserWeb container
function update-nginx(){
    docker cp ./src/TenStar.UserWeb/nginx.conf TenStar.UserWeb:/etc/nginx/nginx.conf
    docker exec TenStar.UserWeb nginx -s reload
}