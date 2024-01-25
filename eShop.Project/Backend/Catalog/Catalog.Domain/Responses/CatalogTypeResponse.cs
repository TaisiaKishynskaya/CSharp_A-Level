namespace Catalog.Domain.Responses;

public record CatalogTypeResponse(
    int Id,
    string Title,
    DateTime CreatedAt,
    DateTime? UpdatedAt
    );
