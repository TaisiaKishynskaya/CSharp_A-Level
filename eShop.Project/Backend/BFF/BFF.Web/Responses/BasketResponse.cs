namespace BFF.Web.Responses;

public class BasketResponse
{
    public string UserId { get; set; } = string.Empty;
    public List<BasketItemResponse> Items { get; set; } = new();
    public decimal TotalPrice { get; set; }
    public int TotalCount { get; set; } 
}
