namespace Catalog.Domain.Models;

public class CatalogType
{
    public int Id { get; set;  }
    public string Title { get; set;  }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
