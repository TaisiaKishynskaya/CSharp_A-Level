namespace Catalog.Application.Infrastructure.Exceptions;

public class ValidationAsyncException : Exception
{
    public ValidationAsyncException(string message) : base(message) { }
}
