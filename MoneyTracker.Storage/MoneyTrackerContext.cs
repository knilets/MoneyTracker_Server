using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Authentication.Entities;
using MoneyTracker.Storage.Models.Entities;

namespace MoneyTracker.Storage;

public class MoneyTrackerContext : IdentityDbContext<ApplicationUser>
{
    public MoneyTrackerContext(DbContextOptions<MoneyTrackerContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Sign> Signs { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Currency>()
            .HasIndex(u => u.Code)
            .IsUnique();
    }
}