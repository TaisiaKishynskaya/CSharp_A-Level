namespace Basket.Domain.Models;

public class BasketItem
{
    public int ItemId { get; set; }
    public string ItemTitle { get; set; } = string.Empty;
    public decimal ItemPrice { get; set; }
    public string PictureUrl { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
