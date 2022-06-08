using Microsoft.AspNetCore.Identity;
using MoneyTracker.Authentication.ApplicationUsers;

namespace MoneyTracker.Authentication.Services;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUserAsync(UserRegistrationRequest request);
    Task<bool> ValidateUserAsync(UserLoginRequest request);
    Task<string> CreateTokenAsync();

    /// <summary>
    /// Returns Id of User by ApplicationUser.Id.
    /// </summary>
    Task<int> GetUserId(string? applicationUserId);
}