#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Osm.FutureValueCalculator.Api/Osm.FutureValueCalculator.Api.csproj", "Osm.FutureValueCalculator.Api/"]
COPY ["Osm.FutureValueCalculator.App/Osm.FutureValueCalculator.App.csproj", "Osm.FutureValueCalculator.App/"]
COPY ["Osm.FutureValueCalculator.Domain/Osm.FutureValueCalculator.Domain.csproj", "Osm.FutureValueCalculator.Domain/"]
RUN dotnet restore "Osm.FutureValueCalculator.Api/Osm.FutureValueCalculator.Api.csproj"
COPY . .
WORKDIR "/src/Osm.FutureValueCalculator.Api"
RUN dotnet build "Osm.FutureValueCalculator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Osm.FutureValueCalculator.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Osm.FutureValueCalculator.Api.dll"]
