
using Catalog.API.Models;

namespace Catalog.API.Services;

public class CatalogBffService : ICatalogBffService
{
    private readonly ICatalogBffRepository _catalogBffRepository;
    private readonly ILogger<CatalogBffService> _logger;

    public CatalogBffService(
        ICatalogBffRepository catalogBffRepository,
        ILogger<CatalogBffService> logger)
    {
        _catalogBffRepository = catalogBffRepository;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogTypeDTO>> GetTypes(int pageIndex, int pageSize)
    {
        var types = await _catalogBffRepository.GetTypes();

        var dtoTypes = types
            .Select(type => new CatalogTypeDTO
            {
                Id = type.Id,
                Type = type.Type,
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var response = new PaginatedItems<CatalogTypeDTO>(pageIndex, pageSize, types.Count(), dtoTypes);

        return response;
    }

    public async Task<PaginatedItems<CatalogBrandDTO>> GetBrands(int pageIndex, int pageSize)
    {
        var brands = await _catalogBffRepository.GetBrands();

        var dtoBrands = brands
            .Select(brand => new CatalogBrandDTO
            {
                Id = brand.Id,
                Brand = brand.Brand,
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var response = new PaginatedItems<CatalogBrandDTO>(pageIndex, pageSize, brands.Count(), dtoBrands);
        return response;
    }

    public async Task<PaginatedItems<CatalogItemDTO>> GetItems(PaginatedItemsRequest request)
    {
        var items = await _catalogBffRepository.GetItems();

        if (request.Types != null && request.Types.Any())
        {
            items = items.Where(item => request.Types.Contains(item.CatalogType.Type));
        }

        if (request.Brands != null && request.Brands.Any())
        {
            items = items.Where(item => request.Brands.Contains(item.CatalogBrand.Brand));
        }

        var dtoItems = items
            .Select(item => new CatalogItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                PictureUri = item.PictureUri,
                TypeName = item.CatalogType.Type,
                BrandName = item.CatalogBrand.Brand,
                AvailableStock = item.AvailableStock
            })
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        var response = new PaginatedItems<CatalogItemDTO>(request.PageIndex, request.PageSize, items.Count(), dtoItems);

        return response;
    }

}
