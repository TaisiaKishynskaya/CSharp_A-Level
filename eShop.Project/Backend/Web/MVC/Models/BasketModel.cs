namespace WebApp.Models;

public class BasketModel
{
    public string UserId { get; set; } = string.Empty;
    public List<BasketItemModel> Items { get; set; } = new List<BasketItemModel>();
    public decimal TotalPrice { get; set; }
    public int TotalCount { get; set; }
}
