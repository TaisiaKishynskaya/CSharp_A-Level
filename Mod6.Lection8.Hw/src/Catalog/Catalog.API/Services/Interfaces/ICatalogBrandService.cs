namespace Catalog.API.Services.Interfaces;

public interface ICatalogBrandService
{
    Task<PaginatedItems<CatalogBrandDTO>> Get(int pageIndex, int pageSize);
    Task<int> Add(CatalogBrandRequest catalogBrandRequest);
    Task<CatalogBrandDTO> Update(CatalogBrand catalogBrand);
    Task<int> Delete(int id);
}

