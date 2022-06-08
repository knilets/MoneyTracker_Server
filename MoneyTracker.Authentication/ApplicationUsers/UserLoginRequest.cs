namespace MoneyTracker.Authentication.ApplicationUsers;

public class UserLoginRequest
{
    public string? UserName { get; init; }
    public string? Password { get; init; }
}