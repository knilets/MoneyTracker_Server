using Microsoft.EntityFrameworkCore.Migrations;
using MoneyTracker.Storage.Helpers;

#nullable disable

namespace MoneyTracker.Storage.Migrations;

public partial class InitializationData : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        const string migrationName = nameof(InitializationData);

        SqlHelper.ExecuteSqlScriptFromEmbeddedResources($"{migrationName}.Insert-Into-Signs.sql", migrationBuilder);
        SqlHelper.ExecuteSqlScriptFromEmbeddedResources($"{migrationName}.Insert-Into-Currencies.sql", migrationBuilder);
        SqlHelper.ExecuteSqlScriptFromEmbeddedResources($"{migrationName}.Insert-Into-Users.sql", migrationBuilder);
        SqlHelper.ExecuteSqlScriptFromEmbeddedResources($"{migrationName}.Insert-Into-Categories.sql", migrationBuilder);
    }
}