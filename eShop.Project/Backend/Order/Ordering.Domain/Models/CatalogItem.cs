namespace Ordering.Domain.Models;

public class CatalogItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PictureFile { get; set; } = string.Empty;
    public CatalogType Type { get; set; } = null!;
    public CatalogBrand Brand { get; set; } = null!;
    public int Quantity { get; set; }
} 
