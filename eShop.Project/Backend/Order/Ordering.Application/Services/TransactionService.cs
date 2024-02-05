using Ordering.Core.Abstractions.Services;
using Ordering.DataAccess.Infrastructure;

namespace Ordering.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly OrderDbContext _dbContext;

    public TransactionService(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ExecuteInTransactionAsync(Func<Task> action)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await action();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
