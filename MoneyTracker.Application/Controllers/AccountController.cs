using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Constants;
using MoneyTracker.Authentication.ApplicationUsers;
using MoneyTracker.Authentication.Services;

namespace MoneyTracker.Application.Controllers;

[Route(RouteConfigs.RoutePrefix)]
public class AccountController : ControllerBase
{
    private readonly IAuthenticationService _service;

    public AccountController(IAuthenticationService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("account/register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationRequest userRegistration)
    {
        var userResult = await _service.RegisterUserAsync(userRegistration);

        return userResult.Succeeded
            ? StatusCode(201)
            : new BadRequestObjectResult(userResult);
    }

    [HttpPost]
    [Route("account/login")]
    public async Task<IActionResult> Authenticate([FromBody] UserLoginRequest user)
    {
        var authenticationResult = await _service.ValidateUserAsync(user);

        return authenticationResult
            ? Ok(new
            {
                Token = await _service.CreateTokenAsync()
            })
            : Unauthorized();
    }
}