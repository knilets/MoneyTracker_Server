using AutoMapper;
using MoneyTracker.Authentication.ApplicationUsers;
using MoneyTracker.Logic.Service.Categories;
using MoneyTracker.Logic.Service.Currencies;
using MoneyTracker.Logic.Service.Transactions;
using MoneyTracker.Logic.Service.Users;
using MoneyTracker.Logic.Service.Wallets;

namespace MoneyTracker.Application.Extensions;

public static class ConfigureMapperExtension
{
    public static void ConfigureMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ApplicationUserMappingProfile());
            mc.AddProfile(new CategoryMappingProfile());
            mc.AddProfile(new CurrencyMappingProfile());
            mc.AddProfile(new TransactionMappingProfile());
            mc.AddProfile(new UserMappingProfile());
            mc.AddProfile(new WalletMappingProfile());
        });

        services.AddSingleton<IMapper>(mapperConfig.CreateMapper());
    }
}