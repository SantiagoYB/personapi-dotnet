# Usar la imagen base de .NET 8 SDK
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Usar la imagen base de .NET 8 SDK para construir
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["personapi-dotnet.csproj", "./"]
RUN dotnet restore "./personapi-dotnet.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "personapi-dotnet.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "personapi-dotnet.csproj" -c Release -o /app/publish

# Ejecutar la aplicación
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "personapi-dotnet.dll"]