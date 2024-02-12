namespace WebApp.Models;

public class CatalogItemModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PictureFile { get; set; } = string.Empty;
    public CatalogTypeModel Type { get; set; } = null!;
    public CatalogBrandModel Brand { get; set; } = null!;
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
}