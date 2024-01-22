namespace Catalog.Data.Entities;

public class CatalogBrandEntity : IEntity
{
    public int Id { get; set; }
    public string Title { get; set; } 

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
