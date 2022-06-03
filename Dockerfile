#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Music.API/Music.Api.csproj", "Music.API/"]
RUN dotnet restore "Music.API/Music.Api.csproj"
COPY . .
WORKDIR "/src/Music.API"
RUN dotnet build "Music.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Music.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Music.Api.dll"]