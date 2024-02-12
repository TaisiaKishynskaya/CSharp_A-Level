namespace Ordering.API.Requests;

public class OrderUpdateRequest
{
    public string? Address { get; set; }
    public List<OrderItemUpdateRequest>? Items { get; set; }
}
