using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Models.Requests.UpdateRequests;
using Catalog.Host.Models.Requests.DeleteRequests;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogTypeRepository
{
    Task<PaginatedItems<CatalogType>> GetTypesByPageAsync(int pageIndex, int pageSize);
    Task<CatalogType> GetTypeByIdAsync(int id);

    Task<PaginatedItems<CatalogType>> GetByPageAsyncHttpGet(int pageIndex, int pageSize);
    Task<int?> AddAsync(AddCatalogTypeRequest typeToAdd);
    Task<CatalogType> UpdateAsync(UpdateCatalogTypeRequest typeToUpdate);
    Task DeleteAsync(DeleteCatalogTypeRequest typeToDelete);
}