using WebApp.Models;

namespace WebApp.Services.Abstractions
{
    public interface ICatalogService
    {
        Task<PaginatedResponse<CatalogItemModel>> GetCatalogItems(int page, int size, string sort, List<int> types = null, List<int> brands = null);
        Task<PaginatedResponse<CatalogTypeModel>> GetCatalogTypes();
        Task<PaginatedResponse<CatalogBrandModel>> GetCatalogBrands();
    }
}