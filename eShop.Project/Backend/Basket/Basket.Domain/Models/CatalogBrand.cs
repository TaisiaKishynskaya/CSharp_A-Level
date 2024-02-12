namespace Basket.Domain.Models;

public record CatalogBrand
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}

