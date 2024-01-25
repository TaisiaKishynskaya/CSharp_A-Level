namespace Catalog.Domain.Models;

public class CatalogItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureFile { get; set; }

    public CatalogType Type { get; set; }
    public CatalogBrand Brand { get; set; }

    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
