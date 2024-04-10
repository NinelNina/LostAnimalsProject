#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Identity/LostAnimals.Identity/LostAnimals.Identity.csproj", "Systems/Identity/LostAnimals.Identity/"]
COPY ["Data/LostAnimals.Context.Entities/LostAnimals.Context.Entities.csproj", "Data/LostAnimals.Context.Entities/"]
COPY ["Data/LostAnimals.Context/LostAnimals.Context.csproj", "Data/LostAnimals.Context/"]
COPY ["Services/LostAnimals.Services.Settings/LostAnimals.Services.Settings.csproj", "Services/LostAnimals.Services.Settings/"]
COPY ["Shared/LostAnimals.Common/LostAnimals.Common.csproj", "Shared/LostAnimals.Common/"]
COPY ["Services/LostAnimals.Services.Logger/LostAnimals.Services.Logger.csproj", "Services/LostAnimals.Services.Logger/"]
RUN dotnet restore "./Systems/Identity/LostAnimals.Identity/LostAnimals.Identity.csproj"
COPY . .
WORKDIR "/src/Systems/Identity/LostAnimals.Identity"
RUN dotnet build "./LostAnimals.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LostAnimals.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LostAnimals.Identity.dll"]