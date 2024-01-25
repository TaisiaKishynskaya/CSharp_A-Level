namespace Catalog.DataAccess.Entities;

public class CatalogItemEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } 
    public decimal Price { get; set; }
    public string PictureFile { get; set; }

    public int? TypeId { get; set; }
    public CatalogTypeEntity Type { get; set; }

    public int? BrandId { get; set; }
    public CatalogBrandEntity Brand { get; set; } 

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
