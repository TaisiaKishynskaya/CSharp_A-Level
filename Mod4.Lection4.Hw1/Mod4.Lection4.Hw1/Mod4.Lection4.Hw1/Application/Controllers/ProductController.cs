using Microsoft.AspNetCore.Mvc;
using Mod4.Lection4.Hw1.Application.DTOs;
using Mod4.Lection4.Hw1.Domain.Interfaces;
using Mod4.Lection4.Hw1.Domain.Models;


namespace Mod4.Lection4.Hw1.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IManagerRepository _managerRepository;
    public ProductController(IManagerRepository managerRepository) => _managerRepository = managerRepository;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var products = await _managerRepository.ProductRepository.GetAllProductsAsync();

        return Ok(products);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var product = await _managerRepository.ProductRepository.GetProductAsync(id);

        if (product == null) return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] ProductDto productParamiter)
    {
        var productId = Guid.NewGuid();
        var product = new Product
        {
            Id = productId,
            Name = productParamiter.Name,
        };

        await _managerRepository.ProductRepository.CreateProductAsync(product);
        await _managerRepository.SaveChangesRepository.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ChangeAsync([FromRoute] Guid id, [FromBody] ProductDto productParamiter)
    {
        var product = await _managerRepository.ProductRepository.GetProductAsync(id);

        if (product == null) return NotFound();

        product.Name = productParamiter.Name;
        
        await _managerRepository.SaveChangesRepository.SaveChangesAsync();

        return Ok(product);
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteAsync(Guid Id)
    {
        var product = await _managerRepository.ProductRepository.GetProductAsync(Id);

        if (product == null) return NotFound();

        await _managerRepository.ProductRepository.DeleteProductAsync(product);
        await _managerRepository.SaveChangesRepository.SaveChangesAsync();

        return StatusCode(204);
    }
}
