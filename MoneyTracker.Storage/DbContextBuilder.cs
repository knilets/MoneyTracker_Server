using Microsoft.EntityFrameworkCore;
using MoneyTracker.Storage.Enums;

namespace MoneyTracker.Storage;

public class DbContextBuilder
{
    public static DbContextOptionsBuilder ConfigureOptions(DbContextOptionsBuilder builder, SqlProviderEnum provider, string connectionString)
    {
        switch (provider)
        {
            case SqlProviderEnum.MsSql:
                builder.UseSqlServer(connectionString);
                break;
            case SqlProviderEnum.PostgreSql:
                builder.UseNpgsql(connectionString);
                break;
            default:
                throw new KeyNotFoundException("Unknown SQL provider.");
        }

        return builder;
    }
}
