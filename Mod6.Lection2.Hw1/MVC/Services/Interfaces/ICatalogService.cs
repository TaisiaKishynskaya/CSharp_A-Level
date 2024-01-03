using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Models.Requests;
using MVC.Models.Responses;
using MVC.ViewModels.CatalogViewModels;

namespace MVC.Services.Interfaces;

public interface ICatalogService
{
    Task<IEnumerable<SelectListItem>> GetBrandsAsync();
    Task<IEnumerable<SelectListItem>> GetTypesAsync();
    Task<PaginatedItemsResponse<CatalogItemViewModel>> GetCatalogItemsAsync(PaginatedItemsRequest request);

}