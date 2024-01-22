namespace Catalog.API.Controllers;

[ApiController]
[Route("api/catalog")]
[Authorize(Policy = "RequireAuthenticatedUser")]
public class CatalogBrandController : ControllerBase
{
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly ILogger<CatalogBrandController> _logger;

    public CatalogBrandController(
        ICatalogBrandService catalogBrandService,
        ILogger<CatalogBrandController> logger)
    {
        _catalogBrandService = catalogBrandService;
        _logger = logger;
    }

    [HttpGet("brands")]
    public async Task<IActionResult> Get([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 5)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        var types = await _catalogBrandService.Get(pageIndex, pageSize);
        return Ok(types);
    }

    [HttpPost("brands")]
    public async Task<IActionResult> Add([FromBody] CatalogBrandRequest request)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        try
        {
            var id = await _catalogBrandService.Add(request);
            return Ok($"Created new brand with id: {id}");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("brands/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CatalogBrandRequest request)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        try
        {
            var updatedCatalogBrand = await _catalogBrandService.Update(new CatalogBrand { Id = id, Brand = request.BrandName });
            return Ok(new { message = $"Brand with id {updatedCatalogBrand.Id} successfully updated", updatedCatalogBrand });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("brands/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        try
        {
            var deletedId = await _catalogBrandService.Delete(id);
            return Ok($"Brand with id {deletedId} successfully deleted");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}

