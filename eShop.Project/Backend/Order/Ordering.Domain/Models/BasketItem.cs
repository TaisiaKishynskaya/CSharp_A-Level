namespace Ordering.Domain.Models;

public class BasketItem
{
    public int ItemId { get; set; }
    public string ItemTitle { get; set; }
    public decimal ItemPrice { get; set; }
    public string PictureUrl { get; set; }
    public int Quantity { get; set; }
}