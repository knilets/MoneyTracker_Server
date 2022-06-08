namespace MoneyTracker.Logic.Currencies;

public interface ICurrencyService
{
    Task<IList<CurrencyDto>> GetAllAsync();
}