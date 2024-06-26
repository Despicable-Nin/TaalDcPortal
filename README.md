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

NOTE: `IntegrationEventLogContext` coexists on the same database (as needed). It is need to be propagated and migrated before running a service (API) that makes use of an AMQP Event Bus. (See `SalesIntegrationEventService` for example.)

#### ON RIDER

Why I can't see my project in a `Startup projects` field?
If you can't see your project in the "Startup project" dropdown, check that your project satisfies the requirements:
`Microsoft.EntityFrameworkCore.Design` or `Microsoft.EntityFrameworkCore.Tools` NuGet package is installed.
Project's TargetFramework is at least `netcoreapp3.1`.

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

#### Snippets
<br/>


```
docker run -d --name sql_server -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=someThingComplicated1234' -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest

```
<br/>

`-d` will launch the container in `"detached"` mode and is optional. This means that containers will run in the background and you can close the terminal window.

`--name` sql_server will assign a name to the container and is optional, but recommended for easier management!

`-e` will allow you to set environment variables:
`'ACCEPT_EULA=Y'` SQL Server requires the user to accept the "End User Licence Agreement" or EULA. The Y here indicates acceptance.

`'SA_PASSWORD=someThingComplicated1234'` is a required parameter for SQL Server.This is the System Administrator password.  <strong><br/>See the note below on password strength.</strong>

`-p 1433:1433` will map the local port 1433 to port 1433 on the container. Port `1433` is the default TCP port that SQL Server will listen on.

`mcr.microsoft.com/mssql/server:2019-latest` is the image we wish to run. I have used the latest version of 2019, however, if you need a different version you can check out the Microsoft SQL Server page on Docker Hub.  

<br/>

### Note on Password Strength
If you find your image starts but then immediately stops or you get an error such as setup failed with error code 1`, then it may be you haven't created a strong enough password. SQL Server really means it when it requests a strong password. Ensure good length with a mixture of upper and lower case, and a mix of alphanumeric characters. For more information on password requirements take a look at the Microsoft documentation.

### Logging

https://blog.datalust.co/using-serilog-in-net-6/

https://hub.docker.com/r/datalust/seq

## RabbitMQ

Running RabbitMQ in Docker
First and foremost, we need to spin up a RabbitMQ server, which we can do simply using Docker. Given that Docker is installed, we’ll open a  command-line terminal and use the docker run command to spin up our server:

```
docker run -d --hostname my-rabbitmq-server --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

We are using the rabbitmq:3-management image from DockerHub which will provide us with a UI, available on port 15672, to view our queues/message throughput, for instance.

We must also add a port mapping for 5672, which is the default port RabbitMQ uses for communication. In order for us to access the management UI, we open a browser window and navigate to localhost:15672, using the default login of guest/guest.
