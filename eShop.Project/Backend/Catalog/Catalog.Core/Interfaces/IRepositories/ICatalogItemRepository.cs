namespace Catalog.Core.Interfaces.IRepositories;

public interface ICatalogItemRepository : IGenericRepository<CatalogItemEntity>
{
    Task<IEnumerable<CatalogItemEntity>> Get(int page, int size);
    Task<CatalogItemEntity> GetById(int id);
    Task<int> Add(CatalogItemEntity entity);
    Task<CatalogItemEntity> Update(CatalogItemEntity entity);
}
