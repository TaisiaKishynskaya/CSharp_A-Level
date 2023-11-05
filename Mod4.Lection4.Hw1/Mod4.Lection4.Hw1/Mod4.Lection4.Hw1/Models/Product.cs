namespace Mod4.Lection4.Hw1.Models;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }

    // *-*
    public required ICollection<OrderItem> ProductItems { get; set; }
}
