namespace Mod4.Lection4.Hw1.Domain.Models;

public class Order
{
    public Guid Id { get; set; }
    public DateTime? Date { get; set; }

    // 1-*
    public User? User { get; set; }

    // *-*
    public ICollection<OrderItem>? OrderItems { get; set; }
}
