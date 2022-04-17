FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["App/BlueHarvest.PoC.CLI/BlueHarvest.PoC.CLI.csproj", "BlueHarvest.PoC.CLI/"]
RUN dotnet restore "App/BlueHarvest.PoC.CLI/BlueHarvest.PoC.CLI.csproj"
COPY . .
WORKDIR "/src/BlueHarvest.PoC.CLI"
RUN dotnet build "BlueHarvest.PoC.CLI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlueHarvest.PoC.CLI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlueHarvest.PoC.CLI.dll"]
