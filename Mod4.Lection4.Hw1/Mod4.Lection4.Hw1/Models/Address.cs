namespace Mod4.Lection4.Hw1.Models;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    // 1-1
    public int UserId { get; set; }
    public User? User { get; set; }
}
