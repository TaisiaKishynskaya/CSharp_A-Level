namespace Basket.Core.Abstractions;

public interface ICatalogService
{
    Task<CatalogItem> GetItemById(int id);
}