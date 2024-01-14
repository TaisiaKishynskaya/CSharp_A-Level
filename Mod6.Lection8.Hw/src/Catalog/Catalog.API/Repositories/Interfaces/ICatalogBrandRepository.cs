namespace Catalog.API.Repositories.Interfaces;

public interface ICatalogBrandRepository
{
    Task<IEnumerable<CatalogBrand>> Get();
    Task<int> Add(CatalogBrand catalogBrand);
    Task Update(CatalogBrand catalogBrand);
    Task Delete(int id);

    Task<CatalogBrand> GetByName(string brandName);
    Task<CatalogBrand> GetById(int id);
}

