namespace Catalog.Core.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly CatalogDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<Type, object> _repositories;
    private readonly IDbContextTransaction _transaction;

    public UnitOfWork(CatalogDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
        _repositories = new Dictionary<Type, object>();
        _transaction = _context.Database.BeginTransaction();
    }

    public void Commit()
    {
        _context.SaveChanges();
        _transaction.Commit();
    }

    public void Rollback()
    {
        _transaction.Rollback();
    }

    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IGenericRepository<TEntity>)_repositories[typeof(TEntity)];
        }

        var factory = (IRepositoryFactory)_serviceProvider.GetService(typeof(IRepositoryFactory))!;
        var repository = factory.CreateRepository<TEntity>();
        _repositories.Add(typeof(TEntity), repository);
        return repository;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
