namespace Mod4.Lection4.Hw1.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Username { get; set; }

    // 1-1
    public Address? UserAddress { get; set; }

    // 1-*
    public ICollection<Order>? Orders { get; set; }
}
