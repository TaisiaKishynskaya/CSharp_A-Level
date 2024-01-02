namespace MVC.Models.Requests;

public class PaginatedItemsRequest
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<int> BrandIds { get; set; }
    public IEnumerable<int> TypeIds { get; set; }
}