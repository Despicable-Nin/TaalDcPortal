FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MarketingApi/Taaldc.Marketing.API/Taaldc.Marketing.API.csproj", "MarketingApi/Taaldc.Marketing.API/"]
RUN dotnet restore "MarketingApi/Taaldc.Marketing.API/Taaldc.Marketing.API.csproj"
COPY . .
WORKDIR "/src/MarketingApi/Taaldc.Marketing.API"
RUN dotnet build "Taaldc.Marketing.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Taaldc.Marketing.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Taaldc.Marketing.API.dll"]
