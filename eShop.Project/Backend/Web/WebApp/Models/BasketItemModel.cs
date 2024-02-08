namespace WebApp.Models;

public class BasketItemModel
{
    public int ItemId { get; set; }
    public string ItemTitle { get; set; }
    public decimal ItemPrice { get; set; }
    public string PictureUrl { get; set; }
    public int Quantity { get; set; }
}
