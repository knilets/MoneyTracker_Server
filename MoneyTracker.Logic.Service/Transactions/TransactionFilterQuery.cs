using MoneyTracker.Logic.Transactions;

namespace MoneyTracker.Logic.Service.Transactions;

public class TransactionFilterQuery
{
    public int? TransactionId { get; private init; }
    public TransactionType? TransactionType { get; private init; }
    public int? CategoryId { get; private init; }
    public int? WalletId { get; private init; }

    /// <summary>
    /// Start Date and Time in UTC.
    /// </summary>
    public DateTime? StartDateTime { get; private init; }

    /// <summary>
    /// End Date and Time in UTC.
    /// </summary>
    public DateTime? EndDateTime { get; private init; }

    /// <summary>
    /// Generates a new instance of TransactionFilterQuery.
    /// </summary>
    public static TransactionFilterQuery ConfigureFilterQuery(int? transactionId = null, TransactionType? transactionType = null, int? categoryId = null, int? walletId = null, DateTime? startDateTime = null, DateTime? endDateTime = null) =>
        new()
        {
            TransactionId = transactionId,
            TransactionType = transactionType,
            CategoryId = categoryId,
            WalletId = walletId,
            StartDateTime = startDateTime?.ToUniversalTime(),
            EndDateTime = endDateTime?.ToUniversalTime()
        };
}