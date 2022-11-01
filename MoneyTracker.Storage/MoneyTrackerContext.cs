using Microsoft.AspNetCore.Identity;
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

        builder.Entity<ApplicationUser>()
            .ToTable("IdentityUsers")
            .Ignore(c => c.AccessFailedCount)
            .Ignore(c => c.LockoutEnabled)
            .Ignore(c => c.TwoFactorEnabled)
            .Ignore(c => c.ConcurrencyStamp)
            .Ignore(c => c.LockoutEnd)
            .Ignore(c => c.EmailConfirmed)
            .Ignore(c => c.TwoFactorEnabled)
            .Ignore(c => c.LockoutEnd)
            .Ignore(c => c.AccessFailedCount)
            .Ignore(c => c.PhoneNumberConfirmed);

        builder.Entity<IdentityRole>()
            .ToTable("IdentityRoles");

        builder.Entity<IdentityUserRole<string>>()
            .ToTable("IdentityUserRoles")
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.Ignore<IdentityUserToken<string>>();
        builder.Ignore<IdentityUserLogin<string>>();
        builder.Ignore<IdentityUserClaim<string>>();
        builder.Ignore<IdentityRoleClaim<string>>();

        builder.Entity<Currency>()
            .HasIndex(u => u.Code)
            .IsUnique();
    }
}