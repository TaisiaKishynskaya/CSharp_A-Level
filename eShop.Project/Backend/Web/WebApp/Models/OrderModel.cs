namespace WebApp.Models;

public class OrderModel
{
    public int Id { get; set; }
    public UserModel User { get; set; } = null!;
    public string Address { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public List<OrderItemModel> Items { get; set; } = null!;
    public decimal TotalPrice => Items?.Sum(item => item.Price * item.Quantity) ?? 0;
}
