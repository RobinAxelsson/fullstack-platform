FROM mcr.microsoft.com/dotnet/sdk:9.0 AS publish
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /publish

FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html
RUN rm -rf ./*
COPY --from=publish /publish/wwwroot .
ENTRYPOINT ["nginx", "-g", "daemon off;"]