namespace Catalog.Core.Abstractions.Repositories;

public interface ICatalogTypeRepository<CatalogTypeEntity> : IRepository<CatalogTypeEntity> where CatalogTypeEntity : class
{
    Task<CatalogTypeEntity> GetByTitle(string title);
}
