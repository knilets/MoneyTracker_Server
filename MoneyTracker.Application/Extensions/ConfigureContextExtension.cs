using Microsoft.EntityFrameworkCore;
using MoneyTracker.Storage;

namespace MoneyTracker.Application.Extensions;

public static class ConfigureContextExtension
{
    public static void ConfigureSqlContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<MoneyTrackerContext>(param => param.UseSqlServer(connectionString));
    }
}