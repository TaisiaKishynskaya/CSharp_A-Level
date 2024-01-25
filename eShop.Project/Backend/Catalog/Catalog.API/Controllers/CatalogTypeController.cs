namespace Catalog.API.Controllers;

[ApiController]
[Route("/api/v1/catalog/types")]
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
        var response = _mapper.Map<IEnumerable<CatalogTypeResponse>>(types);

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

        var response = _mapper.Map<CatalogTypeResponse>(type);

        return Ok(response);
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
        var response = _mapper.Map<CatalogTypeResponse>(updatedType);

        return Ok(response);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteType(int id)
    {
        var result = await _catalogTypeService.Delete(id);
        return Ok(id);
    }
}
