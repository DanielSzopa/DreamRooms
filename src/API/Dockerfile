#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/API/API.csproj", "src/Api/"]
COPY ["src/BuildingBlocks/BuildingBlocks.csproj", "src/BuildingBlocks/"]
COPY ["src/Modules/Reservations/Reservations.Core/Reservations.Core.csproj", "src/Modules/Reservations/Reservations.Core/"]
COPY ["src/Modules/Staff/Staff.Contracts/Staff.Contracts.csproj", "src/Modules/Staff/Staff.Contracts/"]
COPY ["src/Modules/Staff/Staff.Core/Staff.Core.csproj", "src/Modules/Staff/Staff.Core/"]
RUN dotnet restore "./src/Api/./API.csproj"
COPY . .
WORKDIR "/src/src/API"
RUN dotnet build "./API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]