namespace Ordering.Core.Abstractions.Services;

public interface ITransactionService
{
    Task ExecuteInTransactionAsync(Func<Task> action);
}