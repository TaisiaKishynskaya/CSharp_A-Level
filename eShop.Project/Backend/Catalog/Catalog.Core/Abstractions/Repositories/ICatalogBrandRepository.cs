namespace Catalog.Core.Abstractions.Repositories;

public interface ICatalogBrandRepository<CatalogBrandEntity> : IRepository<CatalogBrandEntity> where CatalogBrandEntity : class
{
    Task<CatalogBrandEntity> GetByTitle(string title);
}