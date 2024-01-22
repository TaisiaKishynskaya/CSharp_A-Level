using Catalog.API.Models;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/catalog/bff")]
[Authorize(Policy = "RequireAuthenticatedUser")]
public class CatalogBffController : ControllerBase
{
    private readonly ICatalogBffService _catalogBffService;
    private readonly ILogger<CatalogBffController> _logger;

    public CatalogBffController(
        ICatalogBffService catalogBffService,
        ILogger<CatalogBffController> logger)
    {
        _catalogBffService = catalogBffService;
        _logger = logger;
    }

    [HttpPost("types")]
    public async Task<IActionResult> GetTypes([FromBody] PaginatedRequest request)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        try
        {
            var types = await _catalogBffService.GetTypes(request.PageIndex, request.PageSize);
            return Ok(types);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("brands")]
    public async Task<IActionResult> GetBrands([FromBody] PaginatedRequest request)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        try
        {
            var brands = await _catalogBffService.GetBrands(request.PageIndex, request.PageSize);
            return Ok(brands);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("items")]
    public async Task<IActionResult> GetItems([FromBody] PaginatedItemsRequest request)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        try
        {
            var items = await _catalogBffService.GetItems(request);
            return Ok(items);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
