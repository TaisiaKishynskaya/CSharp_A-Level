namespace BFF.Web.Responses;

public class Order
{
    public int Id { get; set; }
    public User User { get; set; }
    public string Address { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> Items { get; set; }
    public decimal TotalPrice => Items?.Sum(item => item.Price * item.Quantity) ?? 0;
}
