using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Models.Requests.UpdateRequests;
using Catalog.Host.Models.Requests.DeleteRequests;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogBrandRepository
{
    Task<PaginatedItems<CatalogBrand>> GetBrandsByPageAsync(int pageIndex, int pageSize);
    Task<CatalogBrand> GetBrandByIdAsync(int id);

    Task<PaginatedItems<CatalogBrand>> GetByPageAsyncHttpGet(int pageIndex, int pageSize);
    Task<int?> AddAsync(AddCatalogBrandRequest brandToAdd);
    Task<CatalogBrand> UpdateAsync(UpdateCatalogBrandRequest brandToUpdate);
    Task DeleteAsync(DeleteCatalogBrandRequest brandToDelete);
}