namespace Catalog.API.Controllers;

[ApiController]
[Route("/api/v1/catalog/brands")]
public class CatalogBrandController : ControllerBase
{
    private readonly ICatalogBrandService _catalogBrandService;

    public CatalogBrandController(ICatalogBrandService catalogBrandService)
    {
        _catalogBrandService = catalogBrandService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBrands(int page = 1, int size = 3)
    {
        var brands = await _catalogBrandService.Get(page, size);

        var response = brands.Select(brand => new CatalogBrandResponse(
           brand.Id,
           brand.Title,
           brand.CreatedAt,
           brand.UpdatedAt));

        var total = await _catalogBrandService.Count();

        var paginatedResponse = new PaginatedResponse<CatalogBrandResponse>(
            page,
            size,
            total,
            (int)Math.Ceiling(total / (double)size),
            response
        );

        return Ok(paginatedResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBrandById(int id)
    {
        var brand = await _catalogBrandService.GetById(id);

        if (brand == null)
        {
            return BadRequest($"Brand with id = {id} doesn't exist");
        }

        var response = new CatalogBrandResponse(
            brand.Id,
            brand.Title,
            brand.CreatedAt,
            brand.UpdatedAt);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddBrand([FromBody] CatalogBrandRequest request)
    {
        var brand = new CatalogBrand
        {
            Title = request.Title,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        }; 

        var id = await _catalogBrandService.Add(brand);

        return Ok($"Brand with id = {id} was successfully created.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBrand(int id, [FromBody] CatalogBrandRequest request)
    {
        var brand = await _catalogBrandService.Update(id, request);

        var response = new CatalogBrandResponse(
            brand.Id,
            brand.Title,
            brand.CreatedAt,
            brand.UpdatedAt
        );

        return Ok($"Updated brand with id = {response.Id}: {response.Title}, {response.CreatedAt}, {response.UpdatedAt}.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        await _catalogBrandService.Delete(id);
        return Ok($"Brand with id = {id} was successfully deleted.");
    }
}
