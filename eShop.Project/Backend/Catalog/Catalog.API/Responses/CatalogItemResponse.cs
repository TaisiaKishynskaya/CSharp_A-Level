namespace Catalog.API.Responses;

public record CatalogItemResponse(
    int Id,
    string Title,
    string Description,
    decimal Price,
    string PictureFile,
    string Type,
    string Brand,
    int Quantity,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
