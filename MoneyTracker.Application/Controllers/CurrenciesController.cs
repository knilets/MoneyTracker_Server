using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Constants;
using MoneyTracker.Logic.Currencies;

namespace MoneyTracker.Application.Controllers;

[Authorize]
[Route(RouteConfigs.RoutePrefix)]
public class CurrenciesController : Controller
{
    private readonly ICurrencyService _service;

    public CurrenciesController(ICurrencyService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("currencies")]
    public async Task<IActionResult> GetAll()
    {
        var currencies = await _service.GetAllAsync();
        return Ok(currencies);
    }
}