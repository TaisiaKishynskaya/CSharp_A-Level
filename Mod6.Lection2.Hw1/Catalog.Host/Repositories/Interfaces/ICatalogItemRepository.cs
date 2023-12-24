using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);

    Task<int?> Add(CreateProductRequest catalogItem);

    Task<CatalogItem?> GetByIdAsync(int id);

    Task<IEnumerable<CatalogItem>> GetByBrandAsync(string brand);

    Task<IEnumerable<CatalogItem>> GetByTypeAsync(string type);
}
