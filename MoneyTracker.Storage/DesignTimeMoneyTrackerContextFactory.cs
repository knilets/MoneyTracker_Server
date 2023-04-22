using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MoneyTracker.Storage;

/// <summary>
/// Design Time DbContext Factory 
/// <para>This is required to generate migration scripts when DbContext doesn't have constructor with no parameters</para>
/// <para>You can find more information here : https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli</para>
/// <para>Install dotnet ef tools</para>
/// <code>dotnet tool install dotnet-ef</code>
/// <para>You can find all the options by running</para>
/// <code>dotnet ef migrations script --help</code>
/// <para>Generate migration scripts</para>
/// <code>dotnet ef migrations script --idempotent -o ../db/migrations.sql</code>
/// </summary>
public class DesignTimeMoneyTrackerContextFactory : IDesignTimeDbContextFactory<MoneyTrackerContext>
{
    public MoneyTrackerContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MoneyTrackerContext>();
        optionsBuilder.UseSqlServer();

        return new MoneyTrackerContext(optionsBuilder.Options);
    }
}

public class DesignPostgreSqlContextFactory : IDesignTimeDbContextFactory<MoneyTrackerPostgreSqlContext>
{
    public MoneyTrackerPostgreSqlContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MoneyTrackerPostgreSqlContext>();
        optionsBuilder.UseNpgsql();

        return new MoneyTrackerPostgreSqlContext(optionsBuilder.Options);
    }
}