namespace Catalog.Core.Abstractions.Services;
public interface ICatalogItemService
{
    Task<int> Add(CatalogItem item);
    Task<int> Count();
    Task<int> Delete(int id);
    Task<IEnumerable<CatalogItem>> Get(int page, int size);
    Task<CatalogItem> GetById(int id);
    Task<int> Update(CatalogItem item);
}