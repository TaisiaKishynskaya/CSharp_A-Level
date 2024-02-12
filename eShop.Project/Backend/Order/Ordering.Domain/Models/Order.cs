namespace Ordering.Domain.Models;

public class Order
{
    public int Id { get; set; }
    public User User { get; set; } = null!;
    public string Address { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public List<OrderItem> Items { get; set; } = null!;
    public decimal TotalPrice => Items?.Sum(item => item.Price * item.Quantity) ?? 0;
}
