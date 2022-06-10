namespace MoneyTracker.Logic.Wallets;

public class WalletDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public string CurrencyId { get; set; }
    public string CurrencyCode { get; set; }
    public string CurrencySymbol { get; set; }

    public bool IsActive { get; set; }
    public double AvailableFunds { get; set; }
}