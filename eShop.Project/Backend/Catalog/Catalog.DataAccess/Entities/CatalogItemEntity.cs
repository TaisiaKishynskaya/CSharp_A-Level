namespace Catalog.DataAccess.Entities;

public class CatalogItemEntity
{
    public int Id { get; set; }
    public string Title { get; set; } 
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PictureFile { get; set; } = string.Empty;

    public int? TypeId { get; set; }
    public CatalogTypeEntity Type { get; set; } = null!;

    public int? BrandId { get; set; }
    public CatalogBrandEntity Brand { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
