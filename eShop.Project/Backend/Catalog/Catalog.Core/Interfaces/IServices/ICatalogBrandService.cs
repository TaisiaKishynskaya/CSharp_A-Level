namespace Catalog.Core.Interfaces.IServices;

public interface ICatalogBrandService
{
    Task<int> Add(CatalogBrand brand);
    Task<int> Count();
    Task<int> Delete(int id);
    Task<IEnumerable<CatalogBrand>> Get(int page, int size);
    Task<CatalogBrand> GetById(int id);
    Task<CatalogBrand> Update(int id, CatalogBrandRequest request);
}
