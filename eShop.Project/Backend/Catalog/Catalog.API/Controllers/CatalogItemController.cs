namespace Catalog.API.Controllers;

[ApiController]
[Route("/api/v1/catalog/items")]
public class CatalogItemController : Controller
{
    private readonly ICatalogItemService _catalogItemService;

    public CatalogItemController(ICatalogItemService catalogItemService)
    {
        _catalogItemService = catalogItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetItems(int page = 1, int size = 10)
    {
        var items = await _catalogItemService.Get(page, size);

        var response = items.Select(item => new CatalogItemResponse(
           item.Id,
           item.Title,
           item.Description,
           item.Price,
           item.PictureFile,
           item.Type.Title,
           item.Brand.Title,
           item.CreatedAt,
           item.UpdatedAt));

        var total = await _catalogItemService.Count();

        var paginatedResponse = new PaginatedResponse<CatalogItemResponse>(
            page,
            size,
            total,
            (int)Math.Ceiling(total / (double)size),
            response
        );

        return Ok(paginatedResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemById(int id)
    {
        var item = await _catalogItemService.GetById(id);

        if (item == null)
        {
            return BadRequest($"Item with id = {id} doesn't exist.");
        }

        var response = new CatalogItemResponse
        (
            item.Id, 
            item.Title,
            item.Description,
            item.Price,
            item.PictureFile,
            item.Type.Title,
            item.Brand.Title,
            item.CreatedAt,
            item.UpdatedAt
        );
         

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddItem([FromBody] CatalogItemRequest request)
    {
        try
        {
            var item = new CatalogItem
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                PictureFile = request.PictureFile,
                Type = new CatalogType { Title = request.Type.Title },
                Brand = new CatalogBrand { Title = request.Brand.Title },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            var id = await _catalogItemService.Add(item);

            return Ok($"Item with id = {id} was successfully created.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] CatalogItemRequest request)
    {
        try
        {
            var item = new CatalogItem
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                PictureFile = request.PictureFile,
                Type = new CatalogType { Title = request.Type.Title },
                Brand = new CatalogBrand { Title = request.Brand.Title },
                UpdatedAt = DateTime.UtcNow
            };

            var updatedItem = await _catalogItemService.Update(id, item);

            return Ok($"Updated item with id = {updatedItem.Id}:" +
                $" {updatedItem.Title}, " +
                $" {updatedItem.Description}," +
                $" {updatedItem.Price}," +
                $" {updatedItem.PictureFile}," +
                $" {updatedItem.Type}," +
                $" {updatedItem.Brand}," +
                $"{updatedItem.CreatedAt}, " +
                $"{updatedItem.UpdatedAt}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        await _catalogItemService.Delete(id);
        return Ok($"Item with id = {id} was successfully deleted.");
    }
}
