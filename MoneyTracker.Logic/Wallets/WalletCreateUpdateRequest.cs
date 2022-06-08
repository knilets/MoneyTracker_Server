namespace MoneyTracker.Logic.Wallets;

public class WalletCreateUpdateRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public int CurrencyId { get; set; }
    public bool IsActive { get; set; }
}