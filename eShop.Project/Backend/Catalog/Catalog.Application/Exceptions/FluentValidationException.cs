namespace Catalog.Application.Exceptions;

public class FluentValidationException : Exception
{
    FluentValidationException(string message) : base(message) { }
}
