﻿# How to create migration
0. Install dotnet ef tools
```
dotnet tool install --global dotnet-ef
```
1. Open terminal with the current Storage project
2. Run script
```
dotnet ef migrations add %name% --context MoneyTrackerPostgreSqlContext --output-dir PostgreSqlMigrations
```
3. To generate migration script run
```
dotnet ef migrations script -i -o PostgreSqlMigrations/MigrationScript.psql --context MoneyTrackerPostgreSqlContext
```
