namespace BFF.Web.Services.Abstractions;

public interface ICatalogBffService
{
    Task<PaginatedResponse<CatalogBrandResponse>> GetBrands(int page = 1, int size = 3);
    Task<PaginatedResponse<CatalogTypeResponse>> GetTypes(int page = 1, int size = 3);
    Task<PaginatedResponse<CatalogItemResponse>> GetItems(int page = 1, int size = 10);
    Task<CatalogItemResponse> GetItemById(int id);
}