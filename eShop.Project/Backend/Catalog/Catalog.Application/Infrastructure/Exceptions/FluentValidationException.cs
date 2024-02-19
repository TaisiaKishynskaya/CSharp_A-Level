namespace Catalog.Application.Infrastructure.Exceptions;

//  является пользовательским классом исключения, который можно использовать для обработки ошибок, связанных с валидацией данных.

public class FluentValidationException : Exception
{
    // Это конструктор класса FluentValidationException, который принимает строку message.
    // С помощью ключевого слова base(message) вызывается конструктор базового класса Exception с передачей сообщения об ошибке.
    // В этом конструкторе необходимо передавать сообщение об ошибке, которое будет доступно при обработке исключения в коде.
    FluentValidationException(string message) : base(message) { }
}
