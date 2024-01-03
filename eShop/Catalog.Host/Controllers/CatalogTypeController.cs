using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Models.Requests.DeleteRequests;
using Catalog.Host.Models.Requests.UpdateRequests;
using Catalog.Host.Models.Responses.AddResponses;
using Catalog.Host.Models.Responses.UpdateResponses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Host.Controllers;

[ApiController]
[Route($"{ComponentDefaults.DefaultRoute}/catalog")]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogTypeController(
        ILogger<CatalogTypeController> logger,
        ICatalogTypeService catalogTypeService)
    {
        _logger = logger;
        _catalogTypeService = catalogTypeService;
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetTypesByPage(int pageIndex = 1, int pageSize = 10)
    {
        var result = await _catalogTypeService.GetByPageAsyncHttpGet(pageIndex, pageSize);
        return Ok(result);
    }

    [HttpPost("types")]
    [ProducesResponseType(typeof(AddCatalogTypeResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddType(AddCatalogTypeRequest request)
    {
        var result = await _catalogTypeService.AddAsync(request);
        return Ok(new AddCatalogTypeResponse<int?>() { Id = result });
    }

    [HttpPut("types/{id}")]
    [ProducesResponseType(typeof(UpdateCatalogTypeResponse<int>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateType(int id, UpdateCatalogTypeRequest request)
    {
        var result = await _catalogTypeService.UpdateAsync(request);
        return Ok(result);
    }

    [HttpDelete("types/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteType(int id)
    {
        try
        {
            await _catalogTypeService.DeleteAsync(new DeleteCatalogTypeRequest() { Id = id });
            return Ok("Type successfully deleted");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Type not found");
        }
    }
}