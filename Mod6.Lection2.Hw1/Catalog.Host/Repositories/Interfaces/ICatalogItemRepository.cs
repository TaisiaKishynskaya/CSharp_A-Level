using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Models.Requests.DeleteRequests;
using Catalog.Host.Models.Requests.UpdateRequests;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetItemsByPageAsync(PaginatedItemsRequest request);
    Task<CatalogItem> GetItemByIdAsync(int id);
    Task<IEnumerable<CatalogItem>> GetItemsByBrandAsync(int brandId);
    Task<IEnumerable<CatalogItem>> GetItemsByTypeAsync(int typeId);

    Task<PaginatedItems<CatalogItem>> GetByPageAsyncHttpGet(int pageIndex, int pageSize);
    Task<int?> AddAsync(AddCatalogItemRequest itemToAdd);
    Task<CatalogItem> UpdateAsync(UpdateCatalogItemRequest itemToUpdate);
    Task DeleteAsync(DeleteCatalogItemRequest itemToDelete);
}