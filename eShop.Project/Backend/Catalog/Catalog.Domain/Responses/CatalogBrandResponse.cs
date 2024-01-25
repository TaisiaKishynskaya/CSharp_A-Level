namespace Catalog.Domain.Responses;

public record CatalogBrandResponse(
    int Id,
    string Title,
    DateTime CreatedAt,
    DateTime? UpdatedAt
    );
    