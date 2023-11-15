namespace Mod4.Lection4.Hw1.Domain.Models;

public class OrderItem
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }

    // *-*
    public ICollection<Order>? Orders { get; set; }

    // *-*
    public ICollection<Product>? Products { get; set; }
}
