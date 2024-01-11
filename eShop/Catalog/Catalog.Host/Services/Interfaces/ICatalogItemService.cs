using Catalog.Host.Data;
using Catalog.Host.Models.DTOs;
using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Models.Requests.DeleteRequests;
using Catalog.Host.Models.Requests.UpdateRequests;
using Catalog.Host.Models.Responses.UpdateResponses;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogItemService
    {
        Task<PaginatedItems<CatalogGetItemDto>> GetByPageAsyncHttpGet(int pageIndex, int pageSize);
        Task<int?> AddAsync(AddCatalogItemRequest addCatalogItem);
        Task<UpdateCatalogItemResponse<int>> UpdateAsync(UpdateCatalogItemRequest updateCatalogItem);
        Task DeleteAsync(DeleteCatalogItemRequest deleteCatalogItem);
    }
}