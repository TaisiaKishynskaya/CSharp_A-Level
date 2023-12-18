using System.Net;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ILogger<CatalogBrandController> _logger;
    private readonly ICatalogBrandRepository _catalogBrandRepository;

    public CatalogBrandController(
        ILogger<CatalogBrandController> logger,
        ICatalogBrandRepository catalogBrandRepository)
    {
        _logger = logger;
        _catalogBrandRepository = catalogBrandRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CatalogBrand>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBrands()
    {
        var result = await _catalogBrandRepository.GetBrandsAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CatalogBrand catalogBrand)
    {
        var result = await _catalogBrandRepository.Add(catalogBrand);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPut]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(CatalogBrand catalogBrand)
    {
        var result = await _catalogBrandRepository.Update(catalogBrand);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpDelete]
    [ProducesResponseType(typeof(AddItemResponse<bool?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(CatalogBrand catalogBrand)
    {
        var result = await _catalogBrandRepository.Delete(catalogBrand);
        return Ok(new AddItemResponse<bool?>() { Id = result });
    }
}