using Catalog.Host.Configurations;
using Catalog.Host.Models.Requests;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute + "/catalog-bff")]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogBffService _catalogService;
    private readonly IOptions<CatalogConfig> _config;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogBffService catalogService,
        IOptions<CatalogConfig> config)
    {
        _logger = logger;
        _catalogService = catalogService;
        _config = config;
    }

    [HttpPost("items")]
    public async Task<IActionResult> GetItemsByPageAsync([FromBody] PaginatedItemsRequest request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request);
        return Ok(result);
    }

    [HttpGet("item/{id}")]
    public async Task<IActionResult> GetItemByIdAsync(int id)
    {
        var result = await _catalogService.GetItemByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("items/brand/{brandId}")]
    public async Task<IActionResult> GetItemsByBrandAsync(int brandId)
    {
        var result = await _catalogService.GetItemsByBrandAsync(brandId);
        return Ok(result);
    }

    [HttpGet("items/type/{typeId}")]
    public async Task<IActionResult> GetItemsByTypeAsync(int typeId)
    {
        var result = await _catalogService.GetItemsByTypeAsync(typeId);
        return Ok(result);
    }

    //Brand
    [HttpGet("brand/{id}")]
    public async Task<IActionResult> GetBrandByIdAsync(int id)
    {
        var result = await _catalogService.GetBrandByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetBrandsByPageAsync([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _catalogService.GetBrandsByPageAsync(pageIndex, pageSize);
        return Ok(result);
    }

    //Type
    [HttpGet("type/{id}")]
    public async Task<IActionResult> GetTypeByIdAsync(int id)
    {
        var result = await _catalogService.GetTypeByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetTypesByPageAsync([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _catalogService.GetTypesByPageAsync(pageIndex, pageSize);
        return Ok(result);
    }
}