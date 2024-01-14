namespace Catalog.API.Services.Interfaces;

public interface ICatalogBffService
{
    Task<PaginatedItems<CatalogTypeDTO>> GetTypes(int pageIndex, int pageSize);
    Task<PaginatedItems<CatalogBrandDTO>> GetBrands(int pageIndex, int pageSize);
    Task<PaginatedItems<CatalogItemDTO>> GetItems(PaginatedItemsRequest request);
}

