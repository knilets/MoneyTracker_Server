using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Constants;
using MoneyTracker.Application.Controllers.Base;
using MoneyTracker.Authentication.Services;
using MoneyTracker.Logic.Transactions;

namespace MoneyTracker.Application.Controllers;

[Route(RouteConfigs.RoutePrefix)]
public class TransactionsController : UserRelatedBaseController
{
    private readonly ITransactionService _service;

    public TransactionsController(IHttpContextAccessor httpContextAccessor,
        IAuthenticationService authenticationService,
        ITransactionService service)
        : base(httpContextAccessor, authenticationService)
    {
        _service = service;
    }

    [HttpPost]
    [Route("transactions/income")]
    public async Task<IActionResult> CreateIncome([FromBody] TransactionCreateUpdateRequest request)
    {
        var incomeTransaction = await _service.CreateIncomeAsync(request, await GetCurrentUserId());
        return Ok(incomeTransaction);
    }

    [HttpPost]
    [Route("transactions/outcome")]
    public async Task<IActionResult> CreateOutcome([FromBody] TransactionCreateUpdateRequest request)
    {
        var outcomeTransaction = await _service.CreateOutcomeAsync(request, await GetCurrentUserId());
        return Ok(outcomeTransaction);
    }

    [HttpGet]
    [Route("transactions/income")]
    public async Task<IActionResult> GetIncome([FromQuery] int? categoryId, 
        [FromQuery] int? walletId,
        [FromQuery] DateTime? startDateTime,
        [FromQuery] DateTime? endDateTime)
    {
        var transactions = await _service.GetForUserAsync(await GetCurrentUserId(),
            transactionType: TransactionType.Income,
            categoryId: categoryId,
            walletId: walletId,
            startDateTime: startDateTime,
            endDateTime: endDateTime);

        return Ok(transactions);
    }

    [HttpGet]
    [Route("transactions/outcome")]
    public async Task<IActionResult> GetOutcome([FromQuery] int? categoryId, 
        [FromQuery] int? walletId,
        [FromQuery] DateTime? startDateTime,
        [FromQuery] DateTime? endDateTime)
    {
        var transactions = await _service.GetForUserAsync(await GetCurrentUserId(),
            transactionType: TransactionType.Outcome,
            categoryId: categoryId,
            walletId: walletId,
            startDateTime: startDateTime,
            endDateTime: endDateTime);

        return Ok(transactions);
    }

    [HttpGet]
    [Route("transactions")]
    public async Task<IActionResult> GetAll([FromQuery] int? categoryId, 
        [FromQuery] int? walletId,
        [FromQuery] DateTime? startDateTime,
        [FromQuery] DateTime? endDateTime)
    {
        var transactions = await _service.GetForUserAsync(await GetCurrentUserId(),
            categoryId: categoryId,
            walletId: walletId,
            startDateTime: startDateTime,
            endDateTime: endDateTime);

        return Ok(transactions);
    }
}