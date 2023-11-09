namespace Mod4.Lection4.Hw1.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }

    // *-*
    public ICollection<Order> Orders { get; set; } = new List<Order>();

    // *-*
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
