namespace BFF.Web.Responses;

public class PaginatedResponse<TModel>
{
    public int Total { get; set; }
    public IEnumerable<TModel> Data { get; set; } = null!;
}
