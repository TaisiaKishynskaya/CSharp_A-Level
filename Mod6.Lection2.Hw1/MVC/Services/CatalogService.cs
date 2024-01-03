using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MVC.Services.Interfaces;
using MVC.Models.Requests;
using MVC.Models.Responses;
using MVC.ViewModels.CatalogViewModels;

namespace MVC.Services;

public class CatalogService : ICatalogService
{
    private readonly IHttpClientService _httpClientService;
    private readonly AppSettings _appSettings;

    public CatalogService(IHttpClientService httpClientService, IOptions<AppSettings> appSettings)
    {
        _httpClientService = httpClientService;
        _appSettings = appSettings.Value;
    }

    public async Task<PaginatedItemsResponse<CatalogItemViewModel>> GetCatalogItemsAsync(PaginatedItemsRequest request)
    {
        var url = $"{_appSettings.CatalogUrl}/catalog-bff/items";
        var response = await _httpClientService.SendAsync<PaginatedItemsResponse<CatalogItemViewModel>, PaginatedItemsRequest>(url, HttpMethod.Post, request);
        return response;
    }

    public async Task<IEnumerable<SelectListItem>> GetBrandsAsync()
    {
        var url = $"{_appSettings.CatalogUrl}/catalog-bff/brands";
        var response = await _httpClientService.SendAsync<PaginatedBrandResponse, object>(url, HttpMethod.Get, null);
        var brands = response.Data;
        return brands.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Brand });
    }

    public async Task<IEnumerable<SelectListItem>> GetTypesAsync()
    {
        var url = $"{_appSettings.CatalogUrl}/catalog-bff/types";
        var response = await _httpClientService.SendAsync<PaginatedTypeResponse, object>(url, HttpMethod.Get, null);
        var types = response.Data;
        return types.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Type });
    }
}