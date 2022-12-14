# TaalDcPortal

# Local Setup

Run the following on the terminal.

### Adding Migration 
In the sample below, "Initial" is the named instance of the migration.

```
dotnet ef migrations add 
--project CatalogApi/Taaldc.Catalog.Infrastructure/Taaldc.Catalog.Infrastructure.csproj 
--startup-project Taaldc.Mvc/Taaldc.Mvc.csproj 
--context Taaldc.Catalog.Infrastructure.CatalogDbContext --configuration Debug Initial 
--output-dir ../../Taaldc.Mvc/Data/CatalogDbMigration
```

### Update Database
```
dotnet ef database update 
--project CatalogApi/Taaldc.Catalog.Infrastructure/Taaldc.Catalog.Infrastructure.csproj 
--startup-project Taaldc.Mvc/Taaldc.Mvc.csproj 
--context Taaldc.Catalog.Infrastructure.CatalogDbContext --configuration Debug 20221214123342_Initial
```