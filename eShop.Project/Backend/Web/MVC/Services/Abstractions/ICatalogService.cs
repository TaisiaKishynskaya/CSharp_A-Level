namespace WebApp.Services.Abstractions;

public interface ICatalogService
{
    Task<PaginatedDataModel<CatalogItemModel>> GetCatalogItems(int page, int size, string sort, List<int> types = null, List<int> brands = null);
    Task<PaginatedDataModel<CatalogTypeModel>> GetCatalogTypes();
    Task<PaginatedDataModel<CatalogBrandModel>> GetCatalogBrands();
    Task<CatalogItemModel> GetCatalogItemById(int id);
}