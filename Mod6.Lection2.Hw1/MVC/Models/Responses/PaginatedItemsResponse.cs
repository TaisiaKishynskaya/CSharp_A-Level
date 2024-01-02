namespace MVC.Models.Responses;

public class PaginatedItemsResponse<T>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public long Count { get; set; }
    public IEnumerable<T> Data { get; set; }
}