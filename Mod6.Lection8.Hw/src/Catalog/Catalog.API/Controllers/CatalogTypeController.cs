namespace Catalog.API.Controllers;

[ApiController]
[Route("api/catalog")]
[Authorize(Policy = "RequireAuthenticatedUser")]
public class CatalogTypeController : ControllerBase
{
    private readonly ICatalogTypeService _catalogTypeService;
    private readonly ILogger<CatalogTypeController> _logger;

    public CatalogTypeController(
        ICatalogTypeService catalogTypeService,
        ILogger<CatalogTypeController> logger)
    {
        _catalogTypeService = catalogTypeService;
        _logger = logger;
    }

    [HttpGet("types")]
    public async Task<IActionResult> Get([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 5)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);


        var types = await _catalogTypeService.Get(pageIndex, pageSize);
        return Ok(types);
    }

    [HttpPost("types")]
    public async Task<IActionResult> Add([FromBody] CatalogTypeRequest request)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        try
        {
            var id = await _catalogTypeService.Add(request);
            return Ok($"Created new type with id: {id}");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("types/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CatalogTypeRequest request)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        try
        {
            var updatedCatalogType = await _catalogTypeService.Update(new CatalogType { Id = id, Type = request.TypeName });
            return Ok(new { message = $"Type with id {updatedCatalogType.Id} successfully updated", updatedCatalogType });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("types/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        try
        {
            var deletedId = await _catalogTypeService.Delete(id);
            return Ok($"Type with id {deletedId} successfully deleted");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}


