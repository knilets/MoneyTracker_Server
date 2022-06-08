namespace MoneyTracker.Logic.Users;

public interface IUserService
{
    Task<UserDto> CreateAsync(UserCreateRequest request);
    Task<UserDto?> GetAsync(int id);
    Task<IList<UserDto>> GetAllAsync();

    Task DeleteAsync(int id);
}