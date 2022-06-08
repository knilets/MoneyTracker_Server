namespace MoneyTracker.Logic.Wallets;

public interface IWalletService
{
    Task<WalletDto> CreateAsync(WalletCreateUpdateRequest request, int userId);
    Task<WalletDto> GetAllowedForUserAsync(int id, int userId);
    Task<IList<WalletDto>> GetForUserAsync(int userId);
    Task<WalletDto> UpdateAsync(WalletCreateUpdateRequest request, int userId);
    Task DeleteAsync(int id, int userId);
}