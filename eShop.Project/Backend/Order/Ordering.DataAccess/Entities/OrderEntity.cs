namespace Ordering.DataAccess.Entities;

public class OrderEntity
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public UserEntity User { get; set; } = null!;
    public string Address { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public List<OrderItemEntity> Items { get; set; } = null!;
    public decimal TotalPrice => Items?.Sum(item => item.Price * item.Quantity) ?? 0;
}


