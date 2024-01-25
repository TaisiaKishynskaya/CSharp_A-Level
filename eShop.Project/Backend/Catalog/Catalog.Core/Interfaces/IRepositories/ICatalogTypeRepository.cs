namespace Catalog.Core.Interfaces.IRepositories;

public interface ICatalogTypeRepository : IGenericRepository<CatalogTypeEntity>
{
    Task<CatalogTypeEntity> Update(CatalogTypeEntity entity);
}
