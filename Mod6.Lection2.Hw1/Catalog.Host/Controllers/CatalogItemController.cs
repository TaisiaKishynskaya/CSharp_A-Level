using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Models.Requests.DeleteRequests;
using Catalog.Host.Models.Requests.UpdateRequests;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Catalog.Host.Models.Responses.AddResponses;
using Catalog.Host.Models.Responses.UpdateResponses;

namespace Catalog.Host.Controllers;

[ApiController]
[Route($"{ComponentDefaults.DefaultRoute}/catalog")]
public class CatalogItemController : ControllerBase
{
    private readonly ILogger<CatalogItemController> _logger;
    private readonly ICatalogItemService _catalogItemService;

    public CatalogItemController(
        ILogger<CatalogItemController> logger,
        ICatalogItemService catalogItemService)
    {
        _logger = logger;
        _catalogItemService = catalogItemService;
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetByPage(int pageIndex = 1, int pageSize = 10)
    {
        var result = await _catalogItemService.GetByPageAsyncHttpGet(pageIndex, pageSize);
        return Ok(result);
    }

    [HttpPost("items")]
    [ProducesResponseType(typeof(AddCatalogItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddAsync(AddCatalogItemRequest request)
    {
        var result = await _catalogItemService.AddAsync(request);
        return Ok(new AddCatalogItemResponse<int?>() { Id = result });
    }

    [HttpPut("items/{id}")]
    [ProducesResponseType(typeof(UpdateCatalogItemResponse<int>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateItem(int id, UpdateCatalogItemRequest request)
    {
        var result = await _catalogItemService.UpdateAsync(request);
        return Ok(result);
    }

    [HttpDelete("items/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteItem(int id)
    {
        try
        {
            await _catalogItemService.DeleteAsync(new DeleteCatalogItemRequest() { Id = id });
            return Ok("Item successfully deleted");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Item not found");
        }
    }


}