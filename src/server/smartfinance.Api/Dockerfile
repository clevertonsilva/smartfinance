#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["smartfinance.Api/smartfinance.Api.csproj", "smartfinance.Api/"]
COPY ["smartfinance.Application/smartfinance.Application.csproj", "smartfinance.Application/"]
COPY ["smartfinance.Domain/smartfinance.Domain.csproj", "smartfinance.Domain/"]
COPY ["smartfinance.Infra.Data/smartfinance.Infra.Data.csproj", "smartfinance.Infra.Data/"]
COPY ["smartfinance.Infra.Identity/smartfinance.Infra.Identity.csproj", "smartfinance.Infra.Identity/"]
RUN dotnet restore "./smartfinance.Api/smartfinance.Api.csproj"
COPY . .
WORKDIR "/src/smartfinance.Api"
RUN dotnet build "./smartfinance.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./smartfinance.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "smartfinance.Api.dll"]