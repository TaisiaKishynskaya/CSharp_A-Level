namespace BFF.Web.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public UserResponse User { get; set; } = null!;
    public string Address { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public List<OrderItemResponse> Items { get; set; } = null!;
    public decimal TotalPrice => Items?.Sum(item => item.Price * item.Quantity) ?? 0;
}
