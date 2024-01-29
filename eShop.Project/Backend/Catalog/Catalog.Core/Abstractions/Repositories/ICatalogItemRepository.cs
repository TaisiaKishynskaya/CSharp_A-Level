namespace Catalog.Core.Abstractions.Repositories;

public interface ICatalogItemRepository<CatalogItemEntity> : IRepository<CatalogItemEntity> where CatalogItemEntity : class
{
    Task<CatalogItemEntity> GetByTitle(string title);
    Task<CatalogItemEntity> GetByPictureFile(string pictureFile);
}