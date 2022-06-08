using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Constants;
using MoneyTracker.Authentication.Services;
using System.Security.Claims;

namespace MoneyTracker.Application.Controllers.Base;

[Authorize]
public class UserRelatedBaseController : Controller
{
    protected readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthenticationService _service;
    
    protected UserRelatedBaseController(IHttpContextAccessor httpContextAccessor, 
        IAuthenticationService service)
    {
        _httpContextAccessor = httpContextAccessor;
        _service = service;
    }

    protected string? ApplicationUserId => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    protected Task<int> GetCurrentUserId() => _service.GetUserId(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
}
