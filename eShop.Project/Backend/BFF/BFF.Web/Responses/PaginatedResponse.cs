namespace Catalog.API.Responses;

public record PaginatedResponse<TModel>(
    int Total,
    IEnumerable<TModel> Data
);
