using Catalog.Host.Models.DTOs;

namespace Catalog.Host.Models.Responses.UpdateResponses;

public class UpdateCatalogItemResponse<T>
{
    public CatalogGetItemDto Item { get; set; } = null!;
}