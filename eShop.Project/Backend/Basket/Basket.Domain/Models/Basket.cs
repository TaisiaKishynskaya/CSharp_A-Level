namespace Basket.Domain.Models;
public class Basket
{
    public string UserId { get; set; } = string.Empty;
    public List<BasketItem> Items { get; set; } = new();
    public decimal TotalPrice
    {
        get => Items.Sum(item => item.ItemPrice * item.Quantity);
    }
    public int TotalCount
    {
        get => Items.Sum(item => item.Quantity);
    }
}
