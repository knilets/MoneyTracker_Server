using Microsoft.AspNetCore.Identity;
using MoneyTracker.Authentication.Entities;
using MoneyTracker.Storage;

namespace MoneyTracker.Application.Extensions;

public static class ConfigureIdentityExtension
{
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                })
            .AddEntityFrameworkStores<MoneyTrackerContext>()
            .AddDefaultTokenProviders();
    }
}