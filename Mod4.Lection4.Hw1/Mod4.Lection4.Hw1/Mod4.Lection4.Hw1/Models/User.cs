namespace Mod4.Lection4.Hw1.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;

    // 1-1
    public Address? UserAddress { get; set; }

    // 1-*
    public ICollection<Order>? Orders { get; set; }
}
