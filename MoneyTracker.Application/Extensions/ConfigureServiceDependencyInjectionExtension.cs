using MoneyTracker.Authentication.Services;
using MoneyTracker.Logic.Categories;
using MoneyTracker.Logic.Currencies;
using MoneyTracker.Logic.Service.Categories;
using MoneyTracker.Logic.Service.Currencies;
using MoneyTracker.Logic.Service.Transactions;
using MoneyTracker.Logic.Service.Users;
using MoneyTracker.Logic.Service.Wallets;
using MoneyTracker.Logic.Transactions;
using MoneyTracker.Logic.Users;
using MoneyTracker.Logic.Wallets;

namespace MoneyTracker.Application.Extensions;

public static class ConfigureServiceDependencyInjectionExtension
{
    /// <summary>
    /// Extension for configuring DI for Services
    /// </summary>
    public static void ConfigureServiceDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWalletService, WalletService>();
    }
}