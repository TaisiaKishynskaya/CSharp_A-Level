using BFF.Web.Services.Abstractions;
using Catalog.API.Responses;
using Catalog.Domain.Models;
using IdentityModel.Client;
using Newtonsoft.Json;
using System.Net.Http;

namespace BFF.Web.Controllers;

[ApiController]
[Route("/bff/catalog")]
[Authorize(Policy = "ApiScope")]
public class WebBffController : ControllerBase
{
    private readonly ICatalogService _catalogService;

    public WebBffController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetBrands(int page = 1, int size = 3)
    {
        try
        {
            var brands = await _catalogService.GetBrands(page, size);
            return Ok(brands);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetTypes(int page = 1, int size = 3)
    {
        try
        {
            var types = await _catalogService.GetTypes(page, size);
            return Ok(types);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetItems(int page = 1, int size = 10)
    {
        try
        {
            var items = await _catalogService.GetItems(page, size);
            return Ok(items);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
 
}