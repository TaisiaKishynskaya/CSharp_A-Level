namespace Catalog.API.Responses;

public record PaginatedResponse<TModel>(
    int Page,
    int PerPage,
    int Total,
    int TotalPages,
    IEnumerable<TModel> Data
);
