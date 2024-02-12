namespace Ordering.API.Infrastructure.Validations;

public class OrderItemUpdateRequestValidator : AbstractValidator<OrderItemUpdateRequest>
{
    public OrderItemUpdateRequestValidator()
    {
        RuleFor(x => x.ItemId)
            .GreaterThan(0)
            .WithMessage("ItemId must be greater than 0");

        RuleFor(x => x.Quantity)
            .InclusiveBetween(1, 1000)
            .WithMessage("Quantity must be a number between 1 and 1000");
    }
}
