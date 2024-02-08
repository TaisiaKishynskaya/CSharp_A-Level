namespace WebApp.Models;

public class OrderModel
{
    public int Id { get; set; }
    public UserModel User { get; set; }
    public string Address { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemModel> Items { get; set; }
    public decimal TotalPrice => Items?.Sum(item => item.Price * item.Quantity) ?? 0;
}
