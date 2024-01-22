namespace Catalog.Data.Entities;

public class CatalogTypeEntity : IEntity
{
    public int Id { get; set; }
    public string Title { get; set; } 

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
