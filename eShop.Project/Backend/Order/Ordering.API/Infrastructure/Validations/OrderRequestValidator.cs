namespace Ordering.API.Infrastructure.Validations;

public class OrderRequestValidator : AbstractValidator<OrderRequest>
{
    public OrderRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .MinimumLength(10)
            .WithMessage("UserId must not be null, empty, and should contain more than 10 characters");

        RuleFor(x => x.Address)
            .NotNull()
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(200)
            .WithMessage("Address must not be null, empty, and should contain between 10 and 200 characters");
    }
}
