namespace Catalog.Application.Infrastructure.Exceptions;

public class FluentValidationException : Exception
{
    FluentValidationException(string message) : base(message) { }
}
