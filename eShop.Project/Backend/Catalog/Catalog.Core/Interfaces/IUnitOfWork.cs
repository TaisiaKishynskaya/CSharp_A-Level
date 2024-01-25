namespace Catalog.Core.Interfaces;

public interface IUnitOfWork
{
    void Commit();
    void Dispose();
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
    void Rollback();
}
