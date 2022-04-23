FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["App/BlueHarvest.Web.Mgmt/BlueHarvest.Web.Mgmt.csproj", "BlueHarvest.Web.Mgmt/"]
RUN dotnet restore "App/BlueHarvest.Web.Mgmt/BlueHarvest.Web.Mgmt.csproj"
COPY . .
WORKDIR "/src/BlueHarvest.Web.Mgmt"
RUN dotnet build "BlueHarvest.Web.Mgmt.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlueHarvest.Web.Mgmt.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlueHarvest.Web.Mgmt.dll"]
