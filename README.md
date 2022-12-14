# TaalDcPortal
## Projects

### Seedwork
This project contains reusable classes and components to implement value-objects especially when doing domain-driven design
<br/>

### Domain
Contains business entities relating to database persistence with self-evaluating behaviours and guard clauses.
<br/>

### Infrastructure
This contains dependencies that are external in nature -- such as database persistence, email, message queues and pipelines etc.
<br/>

### Mvc (Application)
Entry point of the application -- but for an api-based application this is the API.

## Local Setup

Run the following on the terminal.

#### Adding Migration
<br/>

In the sample below, `Initial` is the named instance of the migration.

```
dotnet ef migrations add 
--project CatalogApi/Taaldc.Catalog.Infrastructure/Taaldc.Catalog.Infrastructure.csproj 
--startup-project Taaldc.Mvc/Taaldc.Mvc.csproj 
--context Taaldc.Catalog.Infrastructure.CatalogDbContext --configuration Debug Initial 
--output-dir ../../Taaldc.Mvc/Data/CatalogDbMigration
```

#### Update Database
<br/>

In the script below, `20221214123342_Initial` is the target migration version.

```
dotnet ef database update 
--project CatalogApi/Taaldc.Catalog.Infrastructure/Taaldc.Catalog.Infrastructure.csproj 
--startup-project Taaldc.Mvc/Taaldc.Mvc.csproj 
--context Taaldc.Catalog.Infrastructure.CatalogDbContext --configuration Debug 20221214123342_Initial
```

#### Legend

`--project migration.csproj`:  project w/c contains the DbContext<br/>
`--startup-project`: start-up project <br/>
`--context` : full name of the context (in the migration.csproj) <br/>
`--configuration`: Debug (or Release) etc. <br/>
`--output-dir`: the path where the migration classes be dumped into.
<br/>
<br/>

#### Connection String

Important to note that `TrustCertificate=true;MultiSubNetFailover=True` must be set if using `Docker` instance of SQL Server

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=taaldb;User Id=xxx;Password=xxxxxx;TrustServerCertificate=True;MultiSubnetFailover=True;",
    "DevConnection":"DataSource=app.db;Cache=Shared"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```

### Setting up Docker (on Mac)
<br/>

See link: https://www.twilio.com/blog/using-sql-server-on-macos-with-docker

<br/>

