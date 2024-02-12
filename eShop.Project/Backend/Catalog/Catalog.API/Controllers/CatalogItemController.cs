namespace Catalog.API.Controllers;

[ApiController]
[Route("/api/v1/catalog/items")]
[Authorize("CatalogApiScope")]
public class CatalogItemController : Controller
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly IMapper _mapper;

    public CatalogItemController(
        ICatalogItemService catalogItemService,
        IMapper mapper)
    {
        _catalogItemService = catalogItemService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetItems(int page = 1, int size = 3)
    {
        var items = await _catalogItemService.Get(page, size);

        var total = await _catalogItemService.Count();

        var paginatedResponse = new PaginatedResponse<CatalogItem>(
            page,
            size,
            total,
            (int)Math.Ceiling(total / (double)size),
            items
        );

        return Ok(paginatedResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemById(int id)
    {
        var item = await _catalogItemService.GetById(id);
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> AddItem([FromBody] CatalogItemRequest request)
    {
        var item = _mapper.Map<CatalogItem>(request);
        item.CreatedAt = DateTime.UtcNow;

        var id = await _catalogItemService.Add(item);

        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] CatalogItemRequest request)
    {
        var item = new CatalogItem
        {
            Id = id,
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            PictureFile = request.PictureFile,
            Quantity = request.Quantity,
            Type = new CatalogType { Title = request.Type.Title },
            Brand = new CatalogBrand { Title = request.Brand.Title }
        };

        var updatedItemId = await _catalogItemService.Update(item);

        return Ok(updatedItemId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var result = await _catalogItemService.Delete(id);
        return Ok(id);
    }
}


