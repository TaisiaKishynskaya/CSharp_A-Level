namespace Catalog.Core.Abstractions.Services;

public interface ICatalogBrandService
{
    Task<int> Add(CatalogBrand type);
    Task<int> Count();
    Task<int> Delete(int id);
    Task<IEnumerable<CatalogBrand>> Get(int page, int size);
    Task<CatalogBrand> GetById(int id);
    Task<CatalogBrand> GetByTitle(string title);
    Task<int> Update(CatalogBrand brand);
}