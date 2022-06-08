using Microsoft.AspNetCore.Identity;
using MoneyTracker.Authentication.Entities;
using MoneyTracker.Storage;

namespace MoneyTracker.Application.Extensions;

public static class ConfigureIdentityExtension
{
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<MoneyTrackerContext>()
            .AddDefaultTokenProviders();
    }
}