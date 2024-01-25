namespace Catalog.Core.Interfaces;

public interface IRepositoryFactory
{
    IGenericRepository<TEntity> CreateRepository<TEntity>() where TEntity : class, IEntity;
}
