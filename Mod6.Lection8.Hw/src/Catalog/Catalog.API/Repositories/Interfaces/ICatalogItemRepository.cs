namespace Catalog.API.Repositories.Interfaces;
public interface ICatalogItemRepository
{
    Task<IEnumerable<CatalogItem>> Get();
    Task<CatalogItem> GetById(int id);
    Task<string> GetPictureUriById(int id);
    Task<int> Add(CatalogItem catalogItem);
    Task Update(CatalogItem catalogItem);
    Task Delete(int id);
}

