#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Systems/Web/LostAnimals.Web/LostAnimals.Web.csproj", "Systems/Web/LostAnimals.Web/LostAnimals.Web.csproj"]
RUN dotnet restore "Systems/Web/LostAnimals.Web/LostAnimals.Web.csproj"
COPY . .
WORKDIR "/src/Systems/Web/LostAnimals.Web"
RUN dotnet build "LostAnimals.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LostAnimals.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine
WORKDIR /var/www/web

COPY --from=publish /app/publish/wwwroot .
COPY Systems/Web/LostAnimals.Web/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80