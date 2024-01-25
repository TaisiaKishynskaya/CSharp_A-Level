namespace Catalog.Application.Exceptions;

public class ValidationAsyncException : Exception
{
    public ValidationAsyncException(string message) : base(message) { }
}
