namespace Catalog.API.Models;

public class PaginatedItems<TModel>(int pageIndex, int pageSize, long count, IEnumerable<TModel> data) where TModel : class
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public long count { get; } = count;
    public IEnumerable<TModel> Data { get; } = data;
}

