namespace Ordering.API.Infrastructure.Validations;

public class OrderUpdateRequestValidator : AbstractValidator<OrderUpdateRequest>
{
    public OrderUpdateRequestValidator()
    {
        RuleFor(x => x.Address)
            .NotNull()
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(200)
            .WithMessage("Address must not be null, empty, and should contain between 10 and 200 characters");

        RuleFor(x => x.Items)
            .NotNull()
            .Must(items => items != null && items.Count > 0)
            .WithMessage("Items must not be null and should contain at least one item");

        RuleForEach(x => x.Items)
            .SetValidator(new OrderItemUpdateRequestValidator())
            .When(x => x.Items != null && x.Items.Any());
    }
}
