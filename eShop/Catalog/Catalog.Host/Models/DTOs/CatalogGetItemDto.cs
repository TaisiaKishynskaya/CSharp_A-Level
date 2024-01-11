namespace Catalog.Host.Models.DTOs;

public class CatalogGetItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureFileName { get; set; }
    public string BrandName { get; set; }
    public string TypeName { get; set; }
}