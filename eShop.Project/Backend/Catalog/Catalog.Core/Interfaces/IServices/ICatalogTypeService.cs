namespace Catalog.Core.Interfaces.IServices;

public interface ICatalogTypeService
{
    Task<int> Add(CatalogType type);
    Task<int> Delete(int id);
    Task<IEnumerable<CatalogType>> Get(int page, int size);
    Task<CatalogType> GetById(int id);
    Task<CatalogType> Update(int id, CatalogTypeRequest request);
    Task<int> Count();
}
