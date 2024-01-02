namespace Catalog.Host.Models.Requests;

public class PaginatedItemsRequest
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public List<int> BrandIds { get; set; }
    public List<int> TypeIds { get; set; }
}