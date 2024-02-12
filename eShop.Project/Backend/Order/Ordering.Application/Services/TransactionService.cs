namespace Ordering.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly OrderDbContext _dbContext;
    private readonly ILogger<TransactionService> _logger;

    public TransactionService(OrderDbContext dbContext, ILogger<TransactionService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task ExecuteInTransactionAsync(Func<Task> action)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await action();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            await transaction.RollbackAsync();
            throw;
        }
    }
}