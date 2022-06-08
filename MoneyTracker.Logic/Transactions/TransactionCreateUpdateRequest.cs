namespace MoneyTracker.Logic.Transactions;

public class TransactionCreateUpdateRequest
{
    public int Id { get; set; }
    public int WalletId { get; set; }
    public int? CategoryId { get; set; }
    public int SignId { get; set; }
    public double Sum { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
}