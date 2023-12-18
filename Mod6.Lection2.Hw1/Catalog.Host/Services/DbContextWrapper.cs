using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.Host.Services;

public class DbContextWrapper<T>(IDbContextFactory<T> dbContextFactory) : IDbContextWrapper<T>
    where T : DbContext
{
    private readonly T _dbContext = dbContextFactory.CreateDbContext();

    public T DbContext => _dbContext;

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }
}