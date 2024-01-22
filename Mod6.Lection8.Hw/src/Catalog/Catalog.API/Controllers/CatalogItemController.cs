namespace Catalog.API.Controllers;

[ApiController]
[Route("api/catalog")]
//[Authorize(Policy = "RequireAuthenticatedUser")]
public class CatalogItemController : ControllerBase
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ILogger<CatalogItemController> _logger;

    public CatalogItemController(
        ICatalogItemService catalogItemService,
        ILogger<CatalogItemController> logger)
    {
        _catalogItemService = catalogItemService;
        _logger = logger;
    }

    [HttpGet("items")]
    public async Task<IActionResult> Get([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 5)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        var items = await _catalogItemService.Get(pageIndex, pageSize);

        return Ok(items);
    }

    [HttpGet("items/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        try
        {
            var item = await _catalogItemService.GetById(id);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("items/{id}/pic")]
    public async Task<IActionResult> GetPicture(int id)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        try
        {
            var imagePath = await _catalogItemService.GetPicturePathById(id);
            var image = System.IO.File.OpenRead(imagePath);
            var pictureUri = await _catalogItemService.GetPictureUriById(id);
            Response.Headers["Picture-Uri"] = pictureUri;
            return File(image, "image/jpeg");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("items")]
    public async Task<IActionResult> Add(CatalogItemRequest catalogItemRequest)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        try
        {
            var id = await _catalogItemService.Add(catalogItemRequest);
            return Ok($"Created new item with id: {id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("items/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CatalogItemRequest request)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        try
        {
            var updatedCatalogItem = await _catalogItemService.Update(new CatalogItem
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                PictureUri = request.PictureUri,
                CatalogTypeId = request.TypeId,
                CatalogBrandId = request.BrandId,
                AvailableStock = request.AvailableStock
            });
            return Ok(new { message = $"Item with id {updatedCatalogItem.Id} successfully updated", updatedCatalogItem });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("items/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid) BadRequest(ModelState);

        try
        {
            var deletedId = await _catalogItemService.Delete(id);
            return Ok($"Item with id {deletedId} successfully deleted");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}

