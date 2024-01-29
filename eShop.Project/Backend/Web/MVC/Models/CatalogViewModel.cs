namespace WebApp.Models;

public class CatalogViewModel
{
    public PaginatedResponse<CatalogItemModel> CatalogItems { get; set; }
    public PaginatedResponse<CatalogTypeModel> CatalogTypes { get; set; }
    public PaginatedResponse<CatalogBrandModel> CatalogBrands { get; set; }
}
