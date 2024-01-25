namespace Catalog.API.Responses;

public record CatalogTypeResponse(
    int Id,
    string Title,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
