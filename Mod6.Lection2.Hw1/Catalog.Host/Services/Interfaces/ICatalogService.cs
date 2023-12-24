using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex);

    Task<CatalogItemDto> GetByIdAsync(int id);

    Task<IEnumerable<CatalogItemDto>> GetByBrandAsync(string brand);

    Task<IEnumerable<CatalogItemDto>> GetByTypeAsync(string type);
}
