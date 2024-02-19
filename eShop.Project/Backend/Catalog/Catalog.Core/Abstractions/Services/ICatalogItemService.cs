namespace Catalog.Core.Abstractions.Services;

// Этот код определяет интерфейс сервиса для работы с сущностями CatalogItem.

public interface ICatalogItemService
{
    Task<int> Add(CatalogItem item); // добавление новой сущности CatalogItem.
    Task<int> Count(); // подсчет общего количества сущностей CatalogItem.
    Task<int> Delete(int id); // удаление сущности CatalogItem по ее идентификатору.
    Task<IEnumerable<CatalogItem>> Get(int page, int size); // получение коллекции сущностей CatalogItem с возможностью пагинации.
    Task<CatalogItem> GetById(int id); // получение сущности CatalogItem по ее идентификатору.
    Task<int> Update(CatalogItem item); // обновление существующей сущности CatalogItem.
}