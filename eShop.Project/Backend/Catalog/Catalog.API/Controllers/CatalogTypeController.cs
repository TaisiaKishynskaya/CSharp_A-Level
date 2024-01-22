namespace Catalog.API.Controllers;

[ApiController]
[Route("/api/v1/catalog/types")]
public class CatalogTypeController : ControllerBase
{
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogTypeController(ICatalogTypeService catalogTypeService)
    {
        _catalogTypeService = catalogTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTypes(int page = 1, int size = 3)
    {
        var types = await _catalogTypeService.Get(page, size);

        var response = types.Select(type => new CatalogTypeResponse(
           type.Id,
           type.Title,
           type.CreatedAt,
           type.UpdatedAt));

        var total = await _catalogTypeService.Count(); 

        var paginatedResponse = new PaginatedResponse<CatalogTypeResponse>(
            page,
            size,
            total,
            (int)Math.Ceiling(total / (double)size),
            response
        );

        return Ok(paginatedResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTypeById(int id)
    {
        var type = await _catalogTypeService.GetById(id);

        if (type == null)
        {
            return BadRequest($"Type with id = {id} doesn't exist.");
        }

        var response = new CatalogTypeResponse(
            type.Id,
            type.Title,
            type.CreatedAt,
            type.UpdatedAt);

        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> AddType([FromBody] CatalogTypeRequest request)
    {
        var type = new CatalogType
        {
            Title = request.Title,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };

        var id = await _catalogTypeService.Add(type);

        return Ok($"Type with id = {id} was successfully created.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateType(int id, [FromBody] CatalogTypeRequest request)
    {
        var type = await _catalogTypeService.Update(id, request);

        var response = new CatalogTypeResponse(
            type.Id,
            type.Title,
            type.CreatedAt,
            type.UpdatedAt
        );

        return Ok($"Updated type with id = {response.Id}: {response.Title}, {response.CreatedAt}, {response.UpdatedAt}.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteType(int id)
    {
        await _catalogTypeService.Delete(id);
        return Ok($"Type with id = {id} was successfully deleted.");
    }
}
