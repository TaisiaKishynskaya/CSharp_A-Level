namespace WebApp.Models;

public class CatalogItemModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureFile { get; set; }
    public CatalogTypeModel Type { get; set; }
    public CatalogBrandModel Brand { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
}