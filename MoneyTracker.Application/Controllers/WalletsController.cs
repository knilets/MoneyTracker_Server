using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Constants;
using MoneyTracker.Application.Controllers.Base;
using MoneyTracker.Authentication.Services;
using MoneyTracker.Logic.Wallets;

namespace MoneyTracker.Application.Controllers;

[Route(RouteConfigs.RoutePrefix)]
public class WalletsController : UserRelatedBaseController
{
    private readonly IWalletService _service;
    
    public WalletsController(IHttpContextAccessor httpContextAccessor,
        IAuthenticationService authenticationService,
        IWalletService service) : base(httpContextAccessor, authenticationService)
    {
        _service = service;
    }

    [HttpPost]
    [Route("wallets")]
    public async Task<IActionResult> Create([FromBody] WalletCreateUpdateRequest createRequest)
    {
        var wallet = await _service.CreateAsync(createRequest, await GetCurrentUserId());
        return Ok(wallet);
    }

    [HttpGet]
    [Route("wallets")]
    public async Task<IActionResult> GetAll()
    {
        var wallets = await _service.GetForUserAsync(await GetCurrentUserId());
        return Ok(wallets);
    }

    [HttpPut]
    [Route("wallets/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] WalletCreateUpdateRequest updateRequest)
    {
        if (id != updateRequest.Id)
            return BadRequest(Messages.IdDoesNotMuch);

        var wallet = await _service.UpdateAsync(updateRequest, await GetCurrentUserId());
        return Ok(wallet);
    }

    [HttpDelete]
    [Route("wallets/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id, await GetCurrentUserId());
        return Ok();
    }
}