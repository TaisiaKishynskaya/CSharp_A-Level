namespace Catalog.API.Services;
public class CatalogBrandService : ICatalogBrandService
{
    private readonly ICatalogBrandRepository _catalogBrandRepository;
    private readonly ILogger<CatalogBrandService> _logger;

    public CatalogBrandService(
        ICatalogBrandRepository catalogBrandRepository, 
        ILogger<CatalogBrandService> logger)
    {
        _catalogBrandRepository = catalogBrandRepository;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogBrandDTO>> Get(int pageIndex, int pageSize)
    {
        var brands = await _catalogBrandRepository.Get();

        var dtoBrands = brands
            .Select(brand => new CatalogBrandDTO
            {
                Id = brand.Id,
                Brand = brand.Brand,
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var response = new PaginatedItems<CatalogBrandDTO>(pageIndex, pageSize, dtoBrands.Count(), dtoBrands);

        return response;
    }

    public async Task<int> Add(CatalogBrandRequest catalogBrandRequest)
    {
        var existingCatalogBrand = await _catalogBrandRepository.GetByName(catalogBrandRequest.BrandName);
        if (existingCatalogBrand != null)
        {
            throw new ArgumentException($"Brand with name {catalogBrandRequest.BrandName} already exists.");
        }

        var catalogBrand = new CatalogBrand { Brand = catalogBrandRequest.BrandName };

        var id = await _catalogBrandRepository.Add(catalogBrand);

        return id;
    }

    public async Task<CatalogBrandDTO> Update(CatalogBrand catalogBrand)
    {
        var existingCatalogBrand = await _catalogBrandRepository.GetById(catalogBrand.Id);
        if (existingCatalogBrand == null)
        {
            throw new ArgumentException($"Brand with id {catalogBrand.Id} does not exist.");
        }

        await _catalogBrandRepository.Update(catalogBrand);

        return new CatalogBrandDTO
        {
            Id = catalogBrand.Id,
            Brand = catalogBrand.Brand
        };
    }

    public async Task<int> Delete(int id)
    {
        var existingCatalogBrand = await _catalogBrandRepository.GetById(id);
        if (existingCatalogBrand == null)
        {
            throw new ArgumentException($"Brand with id {id} does not exist.");
        }

        await _catalogBrandRepository.Delete(id);
        return id;
    }
}

