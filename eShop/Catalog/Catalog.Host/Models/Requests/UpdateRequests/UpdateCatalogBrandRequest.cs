using Catalog.Host.Models.Requests.AddRequests;

namespace Catalog.Host.Models.Requests.UpdateRequests;

public class UpdateCatalogBrandRequest : AddCatalogBrandRequest
{
    public int Id { get; set; }
}