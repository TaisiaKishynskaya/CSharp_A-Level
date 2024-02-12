namespace WebApp.Models;

public class CatalogViewModel
{
    public PaginatedDataModel<CatalogItemModel> CatalogItems { get; set; } = null!;
    public PaginatedDataModel<CatalogTypeModel> CatalogTypes { get; set; } = null!;
    public PaginatedDataModel<CatalogBrandModel> CatalogBrands { get; set; } = null!;
}
