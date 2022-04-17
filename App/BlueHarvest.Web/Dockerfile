FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["App/BlueHarvest.Web/BlueHarvest.Web.csproj", "BlueHarvest.Web/"]
RUN dotnet restore "App/BlueHarvest.Web/BlueHarvest.Web.csproj"
COPY . .
WORKDIR "/src/BlueHarvest.Web"
RUN dotnet build "BlueHarvest.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlueHarvest.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlueHarvest.Web.dll"]
