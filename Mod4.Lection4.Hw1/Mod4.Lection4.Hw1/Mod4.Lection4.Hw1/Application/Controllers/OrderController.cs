using Microsoft.AspNetCore.Mvc;
using Mod4.Lection4.Hw1.Application.DTOs;
using Mod4.Lection4.Hw1.Domain.Interfaces;
using Mod4.Lection4.Hw1.Domain.Models;

namespace Mod4.Lection4.Hw1.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IManagerRepository _managerRepository;
    public OrderController(IManagerRepository managerRepository) => _managerRepository = managerRepository;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var orders = await _managerRepository.OrderRepository.GetAllOrdersAsync();
        
        return Ok(orders);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var order = await _managerRepository.OrderRepository.GetOrderAsync(id);
        
        if (order == null) return NotFound();
        
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] OrderDto orderParamiter)
    {
        var orderId = Guid.NewGuid();
        var order = new Order
        {
            Id = orderId,
            Date = orderParamiter.Date,
        };
        
        await _managerRepository.OrderRepository.CreateOrderAsync(order);
        await _managerRepository.SaveChangesRepository.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ChangeAsync([FromRoute] Guid id, [FromBody] OrderDto orderParamiter)
    {
        var order = await _managerRepository.OrderRepository.GetOrderAsync(id);
        
        if (order == null) return NotFound();

        order.Date = orderParamiter.Date;
        await _managerRepository.SaveChangesRepository.SaveChangesAsync();

        return Ok(order);
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteAsync(Guid Id)
    {
        var order = await _managerRepository.OrderRepository.GetOrderAsync(Id);
        
        if (order == null) return NotFound();

        await _managerRepository.OrderRepository.DeleteOrderAsync(order);
        await _managerRepository.SaveChangesRepository.SaveChangesAsync();

        return StatusCode(204);
    }
}
