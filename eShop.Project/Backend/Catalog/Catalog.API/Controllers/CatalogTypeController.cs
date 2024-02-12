namespace Catalog.API.Controllers;

[ApiController]
[Route("/api/v1/catalog/types")]
[Authorize("CatalogApiScope")]
public class CatalogTypeController : ControllerBase
{
    private readonly ICatalogTypeService _catalogTypeService;
    private readonly IMapper _mapper;

    public CatalogTypeController(
        ICatalogTypeService catalogTypeService, 
        IMapper mapper)
    {
        _catalogTypeService = catalogTypeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTypes(int page = 1, int size = 3)
    {
        var types = await _catalogTypeService.Get(page, size);

        var total = await _catalogTypeService.Count();

        var paginatedResponse = new PaginatedResponse<CatalogType>(
            page,
            size,
            total,
            (int)Math.Ceiling(total / (double)size),
            types
        );

        return Ok(paginatedResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTypeById(int id)
    {
        var type = await _catalogTypeService.GetById(id);
        return Ok(type);
    }


    [HttpPost]
    public async Task<IActionResult> AddType([FromBody] CatalogTypeRequest request)
    {
        var type = _mapper.Map<CatalogType>(request);
        type.CreatedAt = DateTime.UtcNow;

        var id = await _catalogTypeService.Add(type);

        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateType(int id, [FromBody] CatalogTypeRequest request)
    {
        var type = _mapper.Map<CatalogType>(request);
        type.Id = id;

        await _catalogTypeService.Update(type);

        var updatedType = await _catalogTypeService.GetById(id);

        return Ok(updatedType);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteType(int id)
    {
        var result = await _catalogTypeService.Delete(id);
        return Ok(id);
    }
}
