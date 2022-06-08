# Introduction 
MonkeyTracker is an API-Service that helps to manage expencies.

# Structure of Solution
1. `Application` - .NET WEB API Console Application (.NET 6). Contains controllers and endpoints for `MoneyTracker` Service.
    1. `Constans` - Folder with some constant variables.
    2. `Controllers` - Folder with controllers which contains endpoints. Every controller should have name of Entity in plural + `Controller`-postfix.
    3. `Extensions` - Folder with extensions (e.g. `ConfigureContextExtension.cs`).
    4. `Filters` - Folder with attributes which could be used in the Controller-Level like attributes.
    5. `Program.cs` - File with building project configuration. Contains information about dependencies (DI) and rules of WebApplication Builder.
2. `Authentication` - Class Library (.NET 6). Contains Models and Methods related to Identity and Authentication.
    1. `ApplicationUser` - Folder with ApplicationUser related requests and it's mapping profile.
    2. `Entities` - Additional models for Authentication process.
    3. `Service` - Interface and it's implementation of `AuthenticationService`.
3. `Storage.Models` - Class Library (.NET 6). Contains the DataBase Models for the project (not related to concrete DBMS or ORM).
    1. `Entities` - Folder with Database Models.
    2. `Enums` - Folder with Enums.
    3. `Intefaces` - Folder with Model-Intefaces.
4. `Logic` - Class Library (.NET 6). Contains not-related to Database Model and Object Relational Mapping Logic.
    1 - N. `'Entities'` - Folders with Requests (Input Model), IEntityService (Interface with methods for the Entity-Service) and DTOs (Output Model).
5.  `Storage` - Class Library (.NET 6). Contains Database-Context and Migrations.
    1. `Helpers` - Forder with Helper-Classes with code, which needs to be re-used in many places acriss the project.
    2. `Migrations` - Folder with the EntityFramework Migrations.
    3. `Scripts` - Folder with the scripts for Database-Changes, which need to be applied during the migration process.
6. `Logic.Service` - Class Library (.NET 6). Contains implementation of the Service Interfaces from `Logic`. Uses a Database-Context.
    1 - N. `'Entities'` - Folders with `EntityService`(Implementation of `IEntityService`) and EntityMappingProfile.

# Configuration
Configure your own `MoneyTracker.Application/appsettings.json`. There is `example.appsettings.json`. Copy it, rename and put correct configuration.

# Migrations
Migrations are applied automatically from `Program.cs` when the `env = Development`.

## Using Visual Studio
For Migration Process EF-Core Migrations is in use.
Open PacketManagement Console and Select `MoneyTrackeer.Storage` as a Default Project.

Add a new Migration with a name wich desribes a need for such a migration (e.g. `AddColumn2InTable1`).
```
$ Add-Migration AddColumn2InTable1
```

If the build-process and creating the migration goes well, you would see the Migration-Script.
Run command for applying the migration.
```
$ Update-Database
```

## Using CLI
In the project is `MoneyTracker.Storage/DesignTimeMoneyTrackerContextFactory.cs` which helps to generate generate migration scripts.
Read more: `https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli`.
You can install dotnet ef as a local/global tool using the following command:
```
$ dotnet tool install --global dotnet-ef
```
And after that generate SQL Migration Script, which you can run in SQL Server Manually:
```
$ dotnet ef migrations script -i -o migrations.sql
```

# Build and run

Have database available, either locally, or remotely, or in docker container.

Database in docker container can be run like this, using authentication with `sa` login and some password:

```
$ docker run -d --name sql_server_money-tracker -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=money-tracker-has-the-most-complicated-password' -p 1434:1433 mcr.microsoft.com/mssql/server:2019-latest
```

How to build locally:
1. Clone the current repository on your local machine.
2. Open it in the more preferable IDE which supports .NET 6 (Visual Studio, Rider etc).
3. Select `MoneyTracker.Application` as Startup Project.
4. Change `DbConnection` section in the `MoneyTracker.Application/appsettings.Development.json` file (connection string to your SQL server where MoneyTracker Database needs to be installed). See comments in `appsettings.json` to adapt your connection string to docker.
5. Run solution locally.

How to build and run using `docker-compose`.
1. Have docker and docker-compose installed.
2. Make sure that in `docker-compose` you have enabled the correct `DbConnection` property, the one marked with docker compose.
3. `$ docker-compose build` to build in active terminal session. NOTE: Every time when something in appsettings has changed, run this command.
4. `$ docker-compose up` to run in active terminal session or `$ docker-compose up -d` to run in background.
5. Open in browser `http://localhost:7003/swagger/index.html`

# NOTE
1. DateTime values store in UTC.