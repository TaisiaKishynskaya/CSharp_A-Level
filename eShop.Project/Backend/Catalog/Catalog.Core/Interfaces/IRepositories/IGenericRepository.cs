namespace Catalog.Core.Interfaces.IRepositories;

public interface IGenericRepository<TEntity> where TEntity : class, IEntity
{
    Task<int> Add(TEntity entity);
    Task<int> Delete(int id);
    Task<IEnumerable<TEntity>> Get(int page, int size);
    Task<TEntity> GetById(int id);
    Task<TEntity> Update(TEntity entity);
    Task<int> Count();
}
