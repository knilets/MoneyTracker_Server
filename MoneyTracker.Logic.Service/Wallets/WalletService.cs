using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Logic.Wallets;
using MoneyTracker.Storage;
using MoneyTracker.Storage.Models.Entities;
using System.Security.Authentication;

namespace MoneyTracker.Logic.Service.Wallets;

public class WalletService : IWalletService
{
    private readonly MoneyTrackerContext _context;
    private readonly IMapper _mapper;

    public WalletService(MoneyTrackerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WalletDto> CreateAsync(WalletCreateUpdateRequest request, int userId)
    {
        var wallet = _mapper.Map<Wallet>(request);
        wallet.CreatedBy = userId;
        wallet.CreatedAt = DateTime.UtcNow;

        _context.Wallets.Add(wallet);
        await _context.SaveChangesAsync();

        wallet = await GetAllowedAsync(wallet.Id, userId);
        return _mapper.Map<WalletDto>(wallet);
    }

    public async Task<WalletDto> GetAllowedForUserAsync(int id, int userId)
    {
        var wallet = await GetAllowedAsync(id, userId);
        return _mapper.Map<WalletDto>(wallet);
    }

    public async Task<IList<WalletDto>> GetForUserAsync(int userId)
    {
        var wallets = await _context
            .Wallets
            .Include(w => w.Currency)
            .Where(w => w.CreatedBy == userId)
            .ToListAsync();

        return _mapper.Map<IList<WalletDto>>(wallets);
    }

    public async Task<WalletDto> UpdateAsync(WalletCreateUpdateRequest request, int userId)
    {
        var wallet = await GetAllowedAsync(request.Id, userId);

        wallet.Name = request.Name;
        wallet.Description = request.Description;
        wallet.CurrencyId = request.CurrencyId;
        wallet.IsActive = request.IsActive;

        await _context.SaveChangesAsync();

        wallet = await GetAllowedAsync(wallet.Id, userId);
        return _mapper.Map<WalletDto>(wallet);
    }

    public async Task DeleteAsync(int id, int userId)
    {
        var wallet = await GetAllowedAsync(id, userId);

        _context.Wallets.Remove(wallet);
        await _context.SaveChangesAsync();
    }
    
    private async Task<Wallet> GetAllowedAsync(int id, int userId)
    {
        var wallet = await GetOrNotFoundAsync(id);

        if (wallet.CreatedBy != userId)
            throw new AuthenticationException($"You do not have access to the selected Wallet with Identifier '{id}'");

        return wallet;
    }

    private async Task<Wallet> GetOrNotFoundAsync(int id) =>
        await _context.Wallets
            .Include(w => w.Currency)
            .FirstOrDefaultAsync(w => w.Id == id)
        ?? throw new KeyNotFoundException($"Wallet with Identifier '{id}' not found.");
}