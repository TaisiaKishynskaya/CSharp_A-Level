namespace Ordering.DataAccess.Entities;

public class OrderEntity
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public UserEntity User { get; set; }
    public string Address { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<OrderItemEntity> OrderItems { get; set; }
}


