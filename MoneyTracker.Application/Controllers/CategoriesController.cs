using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Application.Constants;
using MoneyTracker.Application.Controllers.Base;
using MoneyTracker.Authentication.Services;
using MoneyTracker.Logic.Categories;

namespace MoneyTracker.Application.Controllers;

[Route(RouteConfigs.RoutePrefix)]
public class CategoriesController : UserRelatedBaseController
{
    private readonly ICategoryService _service;

    public CategoriesController(IHttpContextAccessor httpContextAccessor,
        IAuthenticationService authenticationService,
        ICategoryService service) : base(httpContextAccessor, authenticationService)
    {
        _service = service;
    }

    [HttpPost]
    [Route("categories")]
    public async Task<IActionResult> Create([FromBody] CategoryCreateUpdateRequest createRequest)
    {
        var category = await _service.CreateAsync(createRequest, await GetCurrentUserId());
        return Ok(category);
    }

    [HttpGet]
    [Route("categories")]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _service.GetForUserAsync(await GetCurrentUserId());
        return Ok(categories);
    }

    [HttpPut]
    [Route("categories/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryCreateUpdateRequest updateRequest)
    {
        if (id != updateRequest.Id)
            return BadRequest(Messages.IdDoesNotMuch);

        var category = await _service.UpdateAsync(updateRequest, await GetCurrentUserId());
        return Ok(category);
    }

    [HttpDelete]
    [Route("categories/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id, await GetCurrentUserId());
        return Ok();
    }
}