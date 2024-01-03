using MVC.ViewModels;

namespace MVC.Models.Responses;

public class PaginatedBrandResponse
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public long Count { get; set; }
    public IEnumerable<CatalogBrand> Data { get; set; }
}