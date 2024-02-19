namespace Catalog.Core.Abstractions.Repositories;

// ICatalogItemRepository<CatalogItemEntity> расширяет интерфейс IRepository<CatalogItemEntity> и добавляет дополнительные методы, специфичные для работы с сущностями CatalogItemEntity.
// Ограничение типа where CatalogItemEntity : class указывает, что CatalogItemEntity должен быть классом.

public interface ICatalogItemRepository<CatalogItemEntity> : IRepository<CatalogItemEntity> where CatalogItemEntity : class
{
    Task<CatalogItemEntity> GetByTitle(string title); // получение сущности CatalogItemEntity по ее заголовку.
    Task<CatalogItemEntity> GetByPictureFile(string pictureFile); // получение сущности CatalogItemEntity по файлу изображения.
}