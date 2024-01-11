using Catalog.Host.Data;
using Catalog.Host.Models.DTOs;
using Catalog.Host.Models.Responses;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBffService
    {
        Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageIndex, int pageSize);
        Task<CatalogGetItemDto> GetItemByIdAsync(int id);
        Task<PaginatedItemsResponse<CatalogGetItemDto>> GetItemsByBrandAsync(int brandId);
        Task<PaginatedItemsResponse<CatalogGetItemDto>> GetItemsByTypeAsync(int typeId);

        //Brands
        Task<PaginatedItemsResponse<CatalogBrandDto>> GetBrandsByPageAsync(int pageIndex, int pageSize);
        Task<CatalogBrandDto> GetBrandByIdAsync(int id);

        //Types
        Task<PaginatedItemsResponse<CatalogTypeDto>> GetTypesByPageAsync(int pageIndex, int pageSize);
        Task<CatalogTypeDto> GetTypeByIdAsync(int id);

    }
}