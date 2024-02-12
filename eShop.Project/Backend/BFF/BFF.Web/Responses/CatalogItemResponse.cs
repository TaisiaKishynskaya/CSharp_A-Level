namespace BFF.Web.Responses;

public class CatalogItemResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PictureFile { get; set; } = string.Empty;
    public CatalogTypeResponse Type { get; set; } = null!;
    public CatalogBrandResponse Brand { get; set; } = null!;
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
}