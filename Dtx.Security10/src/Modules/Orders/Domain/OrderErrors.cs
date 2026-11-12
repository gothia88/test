using Dtx.Security.SharedKernel;

namespace Dtx.Orders.Domain;

public static class OrderErrors
{
    public static Error NotFound(Guid orderId) =>
        Error.NotFound("Orders.OrderNotFound", $"Order with Id = '{orderId}' was not found.");

    public static Error EmptyItems() =>
        Error.Validation("Orders.EmptyItems", "An order must contain at least one item.");
}
