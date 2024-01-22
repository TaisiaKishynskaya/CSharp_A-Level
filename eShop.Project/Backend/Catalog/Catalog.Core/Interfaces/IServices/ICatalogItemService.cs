namespace Catalog.Core.Interfaces.IServices;

public interface ICatalogItemService
{
    Task<IEnumerable<CatalogItem>> Get(int page, int size);
    Task<CatalogItem> GetById(int id);
    Task<int> Add(CatalogItem item);
    Task<CatalogItem> Update(int id, CatalogItem item);
    Task<int> Delete(int id);
    Task<int> Count();
}
