using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MoneyTracker.Authentication.ApplicationUsers;
using MoneyTracker.Authentication.Entities;
using MoneyTracker.Logic.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace MoneyTracker.Authentication.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IConfigurationSection _jwtConfiguration;

    private readonly UserManager<ApplicationUser> _userManager;

    private ApplicationUser? _applicationUser;

    public AuthenticationService(IMapper mapper,
        UserManager<ApplicationUser> userManager,
        IUserService userService,
        IConfigurationSection jwtConfiguration)
    {
        _mapper = mapper;
        _userManager = userManager;
        _userService = userService;
        _jwtConfiguration = jwtConfiguration;
    }

    public async Task<IdentityResult> RegisterUserAsync(UserRegistrationRequest request)
    {
        var user = await _userService.CreateAsync(new UserCreateRequest()
        {
            LastName = request.LastName,
            Name = request.FirstName
        });

        var applicationUser = _mapper.Map<ApplicationUser>(request);
        applicationUser.UserId = user.Id;
        
        var result = await _userManager.CreateAsync(applicationUser, request.Password);

        if (!result.Succeeded)
            await _userService.DeleteAsync(user.Id);

        return result;
    }

    public async Task<bool> ValidateUserAsync(UserLoginRequest request)
    {
        var applicationUser = await _userManager.FindByNameAsync(request.UserName);
        if (applicationUser is null)
        {
            return false;
        }

        _applicationUser = applicationUser;

        var user = await _userService.GetAsync(applicationUser.UserId);
        if (user is null)
        {
            return false;
        }

        return await _userManager.CheckPasswordAsync(applicationUser, request.Password);
    }

    public async Task<string> CreateTokenAsync()
    {
        var dateTimeNow = DateTime.UtcNow;
        var dateTimeExpires = dateTimeNow.AddMinutes(Convert.ToDouble(_jwtConfiguration["ExpiresIn"]));

        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(dateTimeNow, dateTimeExpires);
        var tokenOptions = GenerateTokenOptions(dateTimeExpires, signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    public async Task<int> GetUserId(string? applicationUserId)
    {
        if (applicationUserId is null)
            throw new AuthenticationException("This operation is not allowed for the current user.");

        var applicationUser = await _userManager.FindByIdAsync(applicationUserId);
        if (applicationUser is null)
            throw new KeyNotFoundException("User not found.");

        return applicationUser.UserId;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtConfiguration["Secret"]);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims(DateTime dateTimeNow, DateTime dateTimeExpires)
    {
        if (_applicationUser is null)
            return new List<Claim>();

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, _applicationUser.UserName),
            new(ClaimTypes.NameIdentifier, _applicationUser.Id),
            new("IssuedAt", dateTimeNow.ToString("dd.MM.yyyy HH:mm:ss UTC")),
            new("ExpiresAt", dateTimeExpires.ToString("dd.MM.yyyy HH:mm:ss UTC"))
        };
        var roles = await _userManager.GetRolesAsync(_applicationUser);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(DateTime dateTimeExpires, SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken
        (
            issuer: _jwtConfiguration["ValidIssuer"],
            audience: _jwtConfiguration["ValidAudience"],
            claims: claims,
            expires: dateTimeExpires,
            signingCredentials: signingCredentials
        );

        return tokenOptions;
    }
}