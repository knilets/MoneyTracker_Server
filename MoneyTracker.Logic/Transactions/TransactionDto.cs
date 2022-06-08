namespace MoneyTracker.Logic.Transactions;

public class TransactionDto
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public int WalletId { get; set; }
    public string WalletName { get; set; }

    public string CurrencyCode { get; set; }
    public string CurrencySymbol { get; set; }

    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    public int SignId { get; set; }
    public double Sum { get; set; }

    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

}