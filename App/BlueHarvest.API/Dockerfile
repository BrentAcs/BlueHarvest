FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["App/BlueHarvest.API/BlueHarvest.API.csproj", "BlueHarvest.API/"]
RUN dotnet restore "App/BlueHarvest.API/BlueHarvest.API.csproj"
COPY . .
WORKDIR "/src/BlueHarvest.API"
RUN dotnet build "BlueHarvest.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlueHarvest.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlueHarvest.API.dll"]
