using Catalog.Host.Models.DTOs;

namespace Catalog.Host.Models.Responses.UpdateResponses;

public class UpdateCatalogTypeResponse<T>
{
    public CatalogTypeDto Type { get; set; } = null!;
}