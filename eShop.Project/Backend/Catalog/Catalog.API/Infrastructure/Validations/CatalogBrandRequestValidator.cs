namespace Catalog.API.Infrastructure.Validations;

// Этот код представляет собой класс CatalogBrandRequestValidator, который является валидатором для объектов запросов типа CatalogBrandRequest.
// Валидатор использует библиотеку FluentValidation для определения правил валидации.

public class CatalogBrandRequestValidator : AbstractValidator<CatalogBrandRequest>
{
    public CatalogBrandRequestValidator() // В конструкторе класса определены правила валидации для свойства Title объекта CatalogBrandRequest.
    {
        RuleFor(brand => brand.Title)// // казывает, что правила валидации будут применяться к свойству Title.
            .NotEmpty().WithMessage("Title is required") // Задает правило, что свойство Title не должно быть пустым, и в случае нарушения этого правила будет возвращено сообщение об ошибке.
            // Устанавливает ограничение на длину свойства Title от 3 до 50 символов, при нарушении которого будет возвращено сообщение об ошибке.
            .Length(3, 50).WithMessage("Title has to be length between 3 and 50 characters")
            // Проверяет, что значение свойства Title содержит только буквенно-цифровые символы и пробелы. В случае нарушения этого правила будет возвращено сообщение об ошибке.
            .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Title can only contain alphanumeric characters and spaces");
    }
}

// Таким образом, данный валидатор определяет набор правил валидации для свойства Title объекта CatalogBrandRequest, что позволяет проверять его на корректность ввода при обработке запросов в веб-приложении.
