namespace Catalog.API.Infrastructure.Validations;

// Этот код представляет собой класс CatalogItemRequestValidator, который является валидатором для объектов запросов типа CatalogItemRequest.
// Валидатор использует библиотеку FluentValidation для определения правил валидации.

public class CatalogItemRequestValidator : AbstractValidator<CatalogItemRequest>
{
    // В конструкторе класса CatalogItemRequestValidator определены правила валидации для различных свойств объекта CatalogItemRequest.
    public CatalogItemRequestValidator()
    {
        
        RuleFor(item => item.Title) // Для свойства Title определены следующие правила:
            .NotEmpty().WithMessage("Title is required") // Св-во Title не должно быть пустым, иначе возвращается сообщение об ошибке.
            // Длина св-ва Title должна быть 3-50 символов, иначе возвращается сообщение об ошибке.
            .Length(3, 50).WithMessage("Title length has to be between 3 and 50 characters")
            // Св-во Title должно содержать только буквы, цифры и пробелы, иначе возвращается сообщение об ошибке.
            .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Title can only contain alphanumeric characters and spaces");

        // Для св-ва Description определены аналогичные правила валидации, проверяющие его на пустоту и ограничивающие длину 5-500 символов.
        RuleFor(item => item.Description)
            .NotEmpty().WithMessage("Description is required")
            .Length(5, 500).WithMessage("Description length has to be between 20 and 50 characters");

        // Для св-ва PictureFile определены правила валидации, проверяющие на пустоту и на соответствие формата файла (.png).
        RuleFor(item => item.PictureFile)
            .NotEmpty().WithMessage("PictureFile is required")
            .Must(file => file.EndsWith(".png"))
            .WithMessage("PictureFile must be a .png file");

        // Для св-ва Price определены правила валидации, проверяющие его на пустоту и на положительное число.
        RuleFor(item => item.Price)
            .NotEmpty().WithMessage("Price is required")
            .Must(price => price > 0).WithMessage("Price must be a number and greater than 0");

        // Для св-ва Type определены правила валидации, используя другой валидатор CatalogTypeRequestValidator.
        RuleFor(item => item.Type)
          .SetValidator(new CatalogTypeRequestValidator())
          .WithMessage("Invalid Type");

        // Для св-ва Brand определены правила валидации, используя другой валидатор CatalogBrandRequestValidator.
        RuleFor(item => item.Brand)
            .SetValidator(new CatalogBrandRequestValidator())
            .WithMessage("Invalid Brand");

        // Для св-ва Quantity определены правила валидации, проверяющие его на пустоту и на положительное число.
        RuleFor(item => item.Quantity)
            .NotEmpty().WithMessage("Quantity is required")
            .Must(quantity => quantity > 0).WithMessage("Quantity must be a number and greater than 0");
    }
}

// Этот валидатор определяет набор правил валидации для различных св-тв объекта CatalogItemRequest, что позволяет проверять его на корректность ввода при обработке запросов в веб-приложении.
