namespace Catalog.Core.Abstractions.Services;

public interface ICatalogTypeService
{
    Task<int> Add(CatalogType type);
    Task<int> Delete(int id);
    Task<IEnumerable<CatalogType>> Get(int page, int size);
    Task<CatalogType> GetById(int id);
    Task<int> Update(CatalogType type);
    Task<int> Count();
    Task<CatalogType> GetByTitle(string title);
}