namespace MoneyTracker.Logic.Transactions;

public interface ITransactionService
{
    Task<TransactionDto> CreateIncomeAsync(TransactionCreateUpdateRequest request, int userId);
    Task<TransactionDto> CreateOutcomeAsync(TransactionCreateUpdateRequest request, int userId);
    Task<IList<TransactionDto>> GetForUserAsync(int userId, TransactionType? transactionType = null, int? categoryId = null, int? walletId = null, DateTime? startDateTime = null, DateTime? endDateTime = null);
    Task DeleteAsync(int id, int userId);
}