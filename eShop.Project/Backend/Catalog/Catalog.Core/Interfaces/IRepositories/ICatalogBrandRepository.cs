namespace Catalog.Core.Interfaces.IRepositories;

public interface ICatalogBrandRepository : IGenericRepository<CatalogBrandEntity>
{
    Task<CatalogBrandEntity> Update(CatalogBrandEntity entity);
}
