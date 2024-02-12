namespace Basket.API.Requests;

public class ItemRequest
{
    public string UserId { get; set; } = string.Empty;
    public int ItemId { get; set; }
}