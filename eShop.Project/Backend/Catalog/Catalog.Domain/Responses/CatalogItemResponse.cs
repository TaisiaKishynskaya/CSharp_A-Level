namespace Catalog.Domain.Responses;

public record CatalogItemResponse(
    int Id, 
    string Title,
    string Description,
    decimal Price,
    string PictureFile,
    string Type,
    string Brand,
    DateTime CreatedAt,
    DateTime? UpdatedAt
    );
