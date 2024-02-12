namespace Catalog.API.Infrastructure.Validations;

public class CatalogItemRequestValidator : AbstractValidator<CatalogItemRequest>
{
    public CatalogItemRequestValidator()
    {
        RuleFor(item => item.Title)
            .NotEmpty().WithMessage("Title is required")
            .Length(3, 50).WithMessage("Title length has to be between 3 and 50 characters")
            .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Title can only contain alphanumeric characters and spaces");

        RuleFor(item => item.Description)
            .NotEmpty().WithMessage("Description is required")
            .Length(5, 500).WithMessage("Description length has to be between 20 and 50 characters");

        RuleFor(item => item.PictureFile)
            .NotEmpty().WithMessage("PictureFile is required")
            .Must(file => file.EndsWith(".png"))
            .WithMessage("PictureFile must be a .png file");

        RuleFor(item => item.Price)
            .NotEmpty().WithMessage("Price is required")
            .Must(price => price > 0).WithMessage("Price must be a number and greater than 0");

        RuleFor(item => item.Type)
          .SetValidator(new CatalogTypeRequestValidator())
          .WithMessage("Invalid Type");

        RuleFor(item => item.Brand)
            .SetValidator(new CatalogBrandRequestValidator())
            .WithMessage("Invalid Brand");

        RuleFor(item => item.Quantity)
            .NotEmpty().WithMessage("Quantity is required")
            .Must(quantity => quantity > 0).WithMessage("Quantity must be a number and greater than 0");
    }
}
