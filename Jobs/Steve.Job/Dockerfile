﻿FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Jobs/Steve.Job/Steve.Job.csproj", "Jobs/Steve.Job/"]
RUN dotnet restore "Jobs/Steve.Job/Steve.Job.csproj"
COPY . .
WORKDIR "/src/Jobs/Steve.Job"
RUN dotnet build "Steve.Job.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Steve.Job.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Steve.Job.dll"]
