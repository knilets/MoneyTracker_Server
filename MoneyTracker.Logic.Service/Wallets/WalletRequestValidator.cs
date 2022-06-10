using MoneyTracker.Logic.Currencies;
using MoneyTracker.Logic.Wallets;

namespace MoneyTracker.Logic.Service.Wallets;

public class WalletRequestValidator : IRequestValidator<WalletCreateUpdateRequest>
{
    private readonly ICurrencyService _currencyService;

    public WalletRequestValidator(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    public async Task ValidateAsync(WalletCreateUpdateRequest request)
    {
        await _currencyService.GetAsync(request.CurrencyId);
    }
}