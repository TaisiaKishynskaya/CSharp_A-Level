using Catalog.Host.Data;
using Catalog.Host.Models.DTOs;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Models.Requests.DeleteRequests;
using Catalog.Host.Models.Requests.UpdateRequests;
using Catalog.Host.Models.Responses.UpdateResponses;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<PaginatedItems<CatalogBrandDto>> GetByPageAsyncHttpGet(int pageIndex, int pageSize);
        Task<int?> AddAsync(AddCatalogBrandRequest addCatalogBrand);
        Task<UpdateCatalogBrandResponse<int>> UpdateAsync(UpdateCatalogBrandRequest updateCatalogBrand);
        Task DeleteAsync(DeleteCatalogBrandRequest deleteCatalogBrand);
    }
}