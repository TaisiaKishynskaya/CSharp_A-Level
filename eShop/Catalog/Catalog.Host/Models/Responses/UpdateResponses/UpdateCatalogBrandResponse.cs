using Catalog.Host.Models.DTOs;

namespace Catalog.Host.Models.Responses.UpdateResponses;

public class UpdateCatalogBrandResponse<T>
{
    public CatalogBrandDto Brand { get; set; } = null!;
}