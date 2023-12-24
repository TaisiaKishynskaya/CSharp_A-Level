using Catalog.Host.Data;
using Catalog.Host.Models.DTOs;
using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Models.Requests.DeleteRequests;
using Catalog.Host.Models.Requests.UpdateRequests;
using Catalog.Host.Models.Responses.UpdateResponses;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogTypeService
{
    Task<PaginatedItems<CatalogTypeDto>> GetByPageAsyncHttpGet(int pageIndex, int pageSize);
    Task<int?> AddAsync(AddCatalogTypeRequest addCatalogType);
    Task<UpdateCatalogTypeResponse<int>> UpdateAsync(UpdateCatalogTypeRequest updateCatalogType);
    Task DeleteAsync(DeleteCatalogTypeRequest deleteCatalogType);
}