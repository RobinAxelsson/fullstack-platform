FROM mcr.microsoft.com/dotnet/sdk:9.0 AS publish
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
EXPOSE 8088
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8088
WORKDIR /app
COPY --from=publish /publish .
RUN apt update && apt install curl -y
ENTRYPOINT ["dotnet", "TenStar.UserWebApi.dll"]