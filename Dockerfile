FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY . .

EXPOSE 80

EXPOSE 443

ENTRYPOINT ["dotnet", "MusicApi.dll"]
