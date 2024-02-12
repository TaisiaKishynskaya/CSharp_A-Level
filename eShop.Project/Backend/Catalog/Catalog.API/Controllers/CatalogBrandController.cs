﻿namespace Catalog.API.Controllers;

[ApiController]
[Route("/api/v1/catalog/brands")]
[Authorize("CatalogApiScope")]
public class CatalogBrandController : ControllerBase
{
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly IMapper _mapper;

    public CatalogBrandController(
        ICatalogBrandService catalogBrandService,
        IMapper mapper)
    {
        _catalogBrandService = catalogBrandService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetBrands(int page = 1, int size = 3)
    {
        var brands = await _catalogBrandService.Get(page, size);

        var total = await _catalogBrandService.Count();

        var paginatedResponse = new PaginatedResponse<CatalogBrand>(
            page,
            size,
            total,
            (int)Math.Ceiling(total / (double)size),
            brands
        );

        return Ok(paginatedResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBrandById(int id)
    {
        var brand = await _catalogBrandService.GetById(id);
        return Ok(brand);
    }


    [HttpPost]
    public async Task<IActionResult> AddBrand([FromBody] CatalogBrandRequest request)
    {
        var brand = _mapper.Map<CatalogBrand>(request);
        brand.CreatedAt = DateTime.UtcNow;

        var id = await _catalogBrandService.Add(brand);

        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBrand(int id, [FromBody] CatalogBrandRequest request)
    {
        var brand = _mapper.Map<CatalogBrand>(request);
        brand.Id = id;

        await _catalogBrandService.Update(brand);

        var updatedBrand = await _catalogBrandService.GetById(id);

        return Ok(updatedBrand);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        var result = await _catalogBrandService.Delete(id);
        return Ok(id);
    }

}

