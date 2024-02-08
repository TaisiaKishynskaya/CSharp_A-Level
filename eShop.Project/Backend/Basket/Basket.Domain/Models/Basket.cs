namespace Basket.Domain.Models;
public class Basket
{
    public string UserId { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    public decimal TotalPrice
    {
        get => Items.Sum(item => item.ItemPrice * item.Quantity);
    }
    public int TotalCount
    {
        get => Items.Sum(item => item.Quantity);
    }
}
