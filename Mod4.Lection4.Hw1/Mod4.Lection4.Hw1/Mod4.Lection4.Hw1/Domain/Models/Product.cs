namespace Mod4.Lection4.Hw1.Domain.Models;

public class Product
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    // *-*
    public ICollection<OrderItem>? ProductItems { get; set; }
}
