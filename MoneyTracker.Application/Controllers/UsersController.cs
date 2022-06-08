using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Constants;
using MoneyTracker.Logic.Users;

namespace MoneyTracker.Application.Controllers;

[Authorize]
[Route(RouteConfigs.RoutePrefix)]
public class UsersController : Controller
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("users")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }
}