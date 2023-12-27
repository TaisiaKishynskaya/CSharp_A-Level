using Catalog.Host.Models.Requests.AddRequests;

namespace Catalog.Host.Models.Requests.UpdateRequests;

public class UpdateCatalogTypeRequest : AddCatalogTypeRequest
{
    public int Id { get; set; }
}