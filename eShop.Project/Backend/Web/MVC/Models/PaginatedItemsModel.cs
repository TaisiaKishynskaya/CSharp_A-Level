namespace WebApp.Models;

public class PaginatedResponse<T>
{
    public int Total { get; set; }
    public IEnumerable<T> Data { get; set; }
}
