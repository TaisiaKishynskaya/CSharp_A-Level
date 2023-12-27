using Catalog.Host.Models.Requests.AddRequests;

namespace Catalog.Host.Models.Requests.UpdateRequests;

public class UpdateCatalogItemRequest : AddCatalogItemRequest
{
    public int Id { get; set; }
}