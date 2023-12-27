namespace Catalog.Host.Models.DTOs;

public class CatalogItemDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? PictureUrl { get; set; }
}