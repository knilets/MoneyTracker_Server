using Microsoft.EntityFrameworkCore;
using MoneyTracker.Storage;
using MoneyTracker.Storage.Enums;

namespace MoneyTracker.Application.Extensions;

public static class ConfigureContextExtension
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        SqlProviderEnum sqlProvider;
        string connectionString;

        if (configuration.GetValue<bool?>("IsPostgreInUse") ?? false)
        {
            sqlProvider = SqlProviderEnum.PostgreSql;
            connectionString = configuration.GetConnectionString("PostgreSqlConnection")
                               ?? throw new KeyNotFoundException("PostgreSql connection string not found.");
        }
        else
        {
            sqlProvider = SqlProviderEnum.MsSql;
            connectionString = configuration.GetConnectionString("MsSqlConnection")
                               ?? throw new KeyNotFoundException("Ms SQL connection string not found.");
        }

        services.AddDbContext<MoneyTrackerContext>(options =>
            DbContextBuilder.ConfigureOptions(options, sqlProvider, connectionString));
    }
}
