using Microsoft.AspNetCore.Mvc;
using MVC.Models.Requests;
using MVC.Services.Interfaces;

namespace MVC.Controllers;

public class CatalogController : Controller
{
    private readonly ICatalogService _catalogService;
    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
    {
        var request = new PaginatedItemsRequest
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            BrandIds = Request.Cookies["SelectedBrandIds"]?.Split(",").Select(int.Parse).ToList() ?? new List<int> { 0 },
            TypeIds = Request.Cookies["SelectedTypeIds"]?.Split(",").Select(int.Parse).ToList() ?? new List<int> { 0 }
        };
        var items = await _catalogService.GetCatalogItemsAsync(request);
        
        if (items != null)
        {
            ViewBag.Brands = await _catalogService.GetBrandsAsync();
            ViewBag.Types = await _catalogService.GetTypesAsync();
            ViewBag.TotalPages = Math.Ceiling((double)items.Count / items.PageSize);
            ViewBag.PageIndex = items.PageIndex;
        }
        else
        {
            // Обработка случая, когда items равно null
            ViewBag.TotalPages = 0; // или любое другое значение по умолчанию
        }
        
        /*ViewBag.Brands = await _catalogService.GetBrandsAsync();
        ViewBag.Types = await _catalogService.GetTypesAsync();
        ViewBag.TotalPages = Math.Ceiling((double)items.Count / items.PageSize);
        ViewBag.PageIndex = items.PageIndex;*/
        
        return View(items);
    }

    [HttpPost]
    public async Task<IActionResult> Index(PaginatedItemsRequest request)
    {
        request.BrandIds = request.BrandIds ?? new List<int>();
        request.TypeIds = request.TypeIds ?? new List<int>();

        Response.Cookies.Append("SelectedBrandIds", string.Join(",", request.BrandIds));
        Response.Cookies.Append("SelectedTypeIds", string.Join(",", request.TypeIds));

        var items = await _catalogService.GetCatalogItemsAsync(request);
        ViewBag.Brands = await _catalogService.GetBrandsAsync();
        ViewBag.Types = await _catalogService.GetTypesAsync();
        ViewBag.SelectedBrandIds = request.BrandIds;
        ViewBag.SelectedTypeIds = request.TypeIds;
        ViewBag.TotalPages = Math.Ceiling((double)items.Count / items.PageSize);
        ViewBag.PageIndex = items.PageIndex;
        return View(items);
    }

    [HttpPost]
    public IActionResult ResetBrandFilters()
    {
        Response.Cookies.Delete("SelectedBrandIds");
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ResetTypeFilters()
    {
        Response.Cookies.Delete("SelectedTypeIds");
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ResetFilters()
    {
        Response.Cookies.Delete("SelectedBrandIds");
        Response.Cookies.Delete("SelectedTypeIds");
        return RedirectToAction("Index");
    }

    //[HttpPost]
    //public async Task<IActionResult> ResetFiltersAsync()
    //{
    //    Response.Cookies.Delete("SelectedBrandIds");
    //    Response.Cookies.Delete("SelectedTypeIds");

    //    var items = await _catalogService.GetCatalogItemsAsync(new PaginatedItemsRequest());
    //    ViewBag.Brands = await _catalogService.GetBrandsAsync();
    //    ViewBag.Types = await _catalogService.GetTypesAsync();
    //    ViewBag.SelectedBrandIds = new List<int>();
    //    ViewBag.SelectedTypeIds = new List<int>();
    //    if (items != null)
    //    {
    //        ViewBag.TotalPages = Math.Ceiling((double)items.Count / items.PageSize);
    //        ViewBag.PageIndex = items.PageIndex;
    //    }
    //    return View("Index", items);
    //}
}