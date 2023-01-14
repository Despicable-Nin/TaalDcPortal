FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CatalogApi/Taaldc.Catalog.API/Taaldc.Catalog.API.csproj", "CatalogApi/Taaldc.Catalog.API/"]
COPY ["Common/Taaldc.Library.Common/Taaldc.Library.Common.csproj", "Common/Taaldc.Library.Common/"]
COPY ["CatalogApi/Taaldc.Catalog.Infrastructure/Taaldc.Catalog.Infrastructure.csproj", "CatalogApi/Taaldc.Catalog.Infrastructure/"]
COPY ["CatalogApi/Taaldc.Catalog.Domain/Taaldc.Catalog.Domain.csproj", "CatalogApi/Taaldc.Catalog.Domain/"]
COPY ["SeedWork/SeedWork.csproj", "SeedWork/"]
RUN dotnet restore "CatalogApi/Taaldc.Catalog.API/Taaldc.Catalog.API.csproj"
COPY . .
WORKDIR "/src/CatalogApi/Taaldc.Catalog.API"
RUN dotnet build "Taaldc.Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Taaldc.Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Taaldc.Catalog.API.dll"]
