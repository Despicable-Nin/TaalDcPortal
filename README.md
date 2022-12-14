# TaalDcPortal

# Local Setup

Run the following on the terminal.

```
dotnet ef migrations add --project CatalogApi/Taaldc.Catalog.Infrastructure/Taaldc.Catalog.Infrastructure.csproj --startup-project Taaldc.Mvc/Taaldc.Mvc.csproj --context Taaldc.Catalog.Infrastructure.CatalogDbContext --configuration Debug Initial --output-dir ../../Taaldc.Mvc/Data/CatalogDbMigration
```
