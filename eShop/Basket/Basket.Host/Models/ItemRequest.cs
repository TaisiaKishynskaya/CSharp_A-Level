namespace Basket.Host.Models
{
    public class ItemRequest
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
