using System.Net;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private readonly ICatalogTypeRepository _catalogTypeRepository;

    public CatalogTypeController(
        ILogger<CatalogTypeController> logger,
        ICatalogTypeRepository catalogTypeRepository)
    {
        _logger = logger;
        _catalogTypeRepository = catalogTypeRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CatalogType>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTypes()
    {
        var result = await _catalogTypeRepository.GetTypesAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CatalogType catalogType)
    {
        var result = await _catalogTypeRepository.Add(catalogType);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPut]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(CatalogType catalogType)
    {
        var result = await _catalogTypeRepository.Update(catalogType);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpDelete]
    [ProducesResponseType(typeof(AddItemResponse<bool?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(CatalogType catalogType)
    {
        var result = await _catalogTypeRepository.Delete(catalogType);
        return Ok(new AddItemResponse<bool?>() { Id = result });
    }
}