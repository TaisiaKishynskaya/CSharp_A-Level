namespace Catalog.API.Repositories.Interfaces;

public interface ICatalogBffRepository
{
    Task<IEnumerable<CatalogType>> GetTypes();
    Task<IEnumerable<CatalogBrand>> GetBrands();
    Task<IEnumerable<CatalogItem>> GetItems();
}

