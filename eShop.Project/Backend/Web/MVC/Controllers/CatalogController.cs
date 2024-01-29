using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
using WebApp.Services;
using WebApp.Services.Abstractions;

namespace WebApp.Controllers;

public class CatalogController : Controller
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public async Task<IActionResult> Catalog(int page = 1, int size = 5, string sort = "name", List<int> types = null, List<int> brands = null)
    {
        var catalogItems = await _catalogService.GetCatalogItems(page, size, sort, types, brands);
        var catalogTypes = await _catalogService.GetCatalogTypes();
        var catalogBrands = await _catalogService.GetCatalogBrands();

        var model = new CatalogViewModel
        {
            CatalogItems = catalogItems,
            CatalogTypes = catalogTypes,
            CatalogBrands = catalogBrands
        };

        ViewBag.Page = page;
        ViewBag.Size = size;
        ViewBag.Types = types;
        ViewBag.Brands = brands;
        return View(model);
    }
}

