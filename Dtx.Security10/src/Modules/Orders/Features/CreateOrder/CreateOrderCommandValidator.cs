using FluentValidation;

namespace Dtx.Orders.Features.CreateOrder;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.CustomerName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.CustomerEmail).EmailAddress().When(c => !string.IsNullOrWhiteSpace(c.CustomerEmail));
        RuleFor(c => c.Items).NotEmpty();

        RuleForEach(c => c.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ProductName).NotEmpty().MaximumLength(200);
            item.RuleFor(i => i.Quantity).GreaterThan(0);
            item.RuleFor(i => i.UnitPrice).GreaterThanOrEqualTo(0);
        });
    }
}
