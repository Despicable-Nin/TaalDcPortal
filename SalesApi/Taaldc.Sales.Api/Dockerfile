FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SalesApi/Taaldc.Sales.Api/Taaldc.Sales.Api.csproj", "SalesApi/Taaldc.Sales.Api/"]
RUN dotnet restore "SalesApi/Taaldc.Sales.Api/Taaldc.Sales.Api.csproj"
COPY . .
WORKDIR "/src/SalesApi/Taaldc.Sales.Api"
RUN dotnet build "Taaldc.Sales.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Taaldc.Sales.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Taaldc.Sales.Api.dll"]
