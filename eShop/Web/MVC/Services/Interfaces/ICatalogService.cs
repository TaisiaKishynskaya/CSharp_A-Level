using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Services.Interfaces;

public interface ICatalogService
{
    Task<ViewModels.Catalog> GetCatalogItems(int page, int take, int? brand, int? type);
    Task<IEnumerable<SelectListItem>> GetBrands();
    Task<IEnumerable<SelectListItem>> GetTypes();
}