namespace MoneyTracker.Logic.Currencies;

public interface ICurrencyService
{
    Task<IList<CurrencyDto>> GetAllAsync();
    Task<CurrencyDto> GetAsync(int id);
}