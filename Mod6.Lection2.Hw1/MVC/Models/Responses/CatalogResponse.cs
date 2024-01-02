using MVC.ViewModels;
using CatalogItemViewModel = MVC.ViewModels.CatalogViewModels.CatalogItemViewModel;

namespace MVC.Models.Responses;

public class CatalogResponse
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public IEnumerable<CatalogItemViewModel> Data { get; set; }
}