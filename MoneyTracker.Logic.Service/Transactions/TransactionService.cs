using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Logic.Categories;
using MoneyTracker.Logic.Transactions;
using MoneyTracker.Logic.Wallets;
using MoneyTracker.Storage;
using MoneyTracker.Storage.Models.Entities;
using MoneyTracker.Storage.Models.Enums;

namespace MoneyTracker.Logic.Service.Transactions;

public class TransactionService : ITransactionService
{
    private readonly MoneyTrackerContext _context;

    private readonly ICategoryService _categoryService;
    private readonly IWalletService _walletService;

    private readonly IMapper _mapper;

    public TransactionService(MoneyTrackerContext context,
        ICategoryService categoryService,
        IWalletService walletService,
        IMapper mapper)
    {
        _context = context;
        _categoryService = categoryService;
        _walletService = walletService;
        _mapper = mapper;
    }

    public async Task<TransactionDto> CreateIncomeAsync(TransactionCreateUpdateRequest request, int userId)
    {
        request.SignId = (int)SignType.Plus;
        return await CreateAsync(request, userId);
    }

    public async Task<TransactionDto> CreateOutcomeAsync(TransactionCreateUpdateRequest request, int userId)
    {
        request.SignId = (int)SignType.Minus;
        return await CreateAsync(request, userId);
    }

    public async Task<IList<TransactionDto>> GetForUserAsync(int userId,
        TransactionType? transactionType = null,
        int? categoryId = null,
        int? walletId = null,
        DateTime? startDateTime = null,
        DateTime? endDateTime = null)
    {
        var filter = TransactionFilterQuery.ConfigureFilterQuery(
            transactionType: transactionType,
            walletId: walletId,
            categoryId: categoryId,
            startDateTime: startDateTime,
            endDateTime: endDateTime);

        var transactions = await GetTransactionsForUserAsync(userId, filter);
        return _mapper.Map<IList<TransactionDto>>(transactions);
    }

    private async Task<IList<Transaction>> GetTransactionsForUserAsync(int userId, TransactionFilterQuery? filter = null)
    {
        var query = _context.Transactions
            .Where(t => t.UserId == userId && t.Wallet.IsActive);

        if (filter?.TransactionId != null)   
            query = query.Where(t => t.Id == filter.TransactionId);
        else
        {
            switch (filter?.TransactionType)
            {
                case null:
                    break;
                case TransactionType.Income:
                    query = query.Where(t => t.SignId == SignType.Plus);
                    break;
                case TransactionType.Outcome:
                    query = query.Where(t => t.SignId == SignType.Minus);
                    break;
            }

            if (filter?.CategoryId != null)
            {
                _ = await _categoryService.GetAllowedForUserAsync(filter.CategoryId.Value, userId);
                query = query.Where(t => t.CategoryId == filter.CategoryId.Value);
            }

            if (filter?.WalletId != null)
            {
                _ = await _walletService.GetAllowedForUserAsync(filter.WalletId.Value, userId);
                query = query.Where(t => t.WalletId == filter.WalletId.Value);
            }

            if (filter?.StartDateTime != null)
                query = query.Where(t => t.CreatedAt >= filter.StartDateTime);

            if (filter?.EndDateTime != null)
                query = query.Where(t => t.CreatedAt <= filter.EndDateTime);
        }

        return await query
            .Include(t => t.Category)
            .Include(t => t.Wallet)
            .ThenInclude(w => w.Currency)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

    private async Task<Transaction> GetForUserAsync(int id, int userId)
    {
        var filter = TransactionFilterQuery.ConfigureFilterQuery(transactionId: id);
        var transactions = await GetTransactionsForUserAsync(userId, filter);

        return transactions.FirstOrDefault()
               ?? throw new KeyNotFoundException($"Transaction with Id '{id}' not found.");
    }

    private async Task<TransactionDto> CreateAsync(TransactionCreateUpdateRequest request, int userId)
    {
        var transaction = _mapper.Map<Transaction>(request);
        transaction.UserId = userId;

        _ = await _walletService.GetAllowedForUserAsync(request.WalletId, userId);
        _ = await _categoryService.GetAllowedForUserAsync(request.CategoryId ?? 0, userId);

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        transaction = await GetForUserAsync(transaction.Id, userId);
        return _mapper.Map<TransactionDto>(transaction);
    }
}