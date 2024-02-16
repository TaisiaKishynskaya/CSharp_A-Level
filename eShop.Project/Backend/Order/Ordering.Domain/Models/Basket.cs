namespace Ordering.Domain.Models;

public class Basket
{
    public string UserId { get; set; }
    public List<BasketItem> Items { get; set; } = new();
    public decimal TotalPrice
    {
        get => Items.Sum(item => item.ItemPrice);
    }
    public int TotalCount
    {
        get => Items.Sum(item => item.Quantity);
    }
}
