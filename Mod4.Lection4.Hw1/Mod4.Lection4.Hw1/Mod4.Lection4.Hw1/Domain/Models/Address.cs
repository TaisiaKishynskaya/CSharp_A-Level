namespace Mod4.Lection4.Hw1.Domain.Models;

public class Address
{
    public Guid Id { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }

    // 1-1
    public Guid UserId { get; set; }
    public User? User { get; set; }
}
