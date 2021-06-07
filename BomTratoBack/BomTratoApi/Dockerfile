#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["BomTratoApi/BomTratoApi.csproj", "BomTratoApi/"]
COPY ["BomTratoApp/BomTratoApp.csproj", "BomTratoApp/"]
COPY ["BomTratoData/BomTratoData.csproj", "BomTratoData/"]
COPY ["BomTratoDomain/BomTratoDomain.csproj", "BomTratoDomain/"]
RUN dotnet restore "BomTratoApi/BomTratoApi.csproj"
COPY . .
WORKDIR "/src/BomTratoApi"
RUN dotnet build "BomTratoApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BomTratoApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BomTratoApi.dll"]
