namespace Mod4.Lection4.Hw1.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime? Date { get; set; }

    // 1-*
    public required User User { get; set; }

    // *-*
    public required ICollection<OrderItem> OrderItems { get; set; }
}
