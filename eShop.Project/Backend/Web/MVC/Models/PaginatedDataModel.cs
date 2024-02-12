namespace WebApp.Models;

public class PaginatedDataModel<T>
{
    public int Total { get; set; }
    public IEnumerable<T> Data { get; set; } = null!;
}
