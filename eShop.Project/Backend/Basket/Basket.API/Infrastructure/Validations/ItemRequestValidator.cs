namespace Basket.API.Infrastructure.Validations;

public class ItemRequestValidator : AbstractValidator<ItemRequest>
{
    public ItemRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .MinimumLength(10)
            .WithMessage("UserId must not be null, empty, and should contain more than 10 characters");

        RuleFor(x => x.ItemId)
            .GreaterThan(0)
            .WithMessage("ItemId must be greater than 0");
    }
}
