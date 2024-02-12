namespace Basket.Domain.Models;

public class CatalogItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PictureFile { get; set; } = string.Empty;
    CatalogType CatalogType { get; set; } = null!;
    CatalogBrand CatalogBrand { get; set; } = null!;
}
