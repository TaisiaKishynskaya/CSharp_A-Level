using Microsoft.AspNetCore.Mvc;
using Mod4.Lection4.Hw1.Application.DTOs;
using Mod4.Lection4.Hw1.Domain.Interfaces;
using Mod4.Lection4.Hw1.Domain.Models;

namespace Mod4.Lection4.Hw1.Application.Controllers;


[Route("api/[controller]")]
[ApiController]
public class OrderItemController : ControllerBase
{
    private readonly IManagerRepository _managerRepository;
    public OrderItemController(IManagerRepository managerRepository) => _managerRepository = managerRepository;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var orderItems = await _managerRepository.OrderItemRepository.GetAllItemsAsync();
        
        return Ok(orderItems);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdAsync(Guid orderitemId)
    {
        var orderItem = await _managerRepository.OrderItemRepository.GetOrderItemAsync(orderitemId);
        
        if (orderItem == null) return NotFound();
        
        return Ok(orderItem);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] OrderItemDto orderItemDto)
    {
        var orderitemId = Guid.NewGuid();
        
        var orderitem = new OrderItem
        {
            Id = orderitemId,
            Quantity = orderItemDto.Quantity

        };
        
        await _managerRepository.OrderItemRepository.CreateOrderItemAsync(orderitem);
        await _managerRepository.SaveChangesRepository.SaveChangesAsync();

        return StatusCode(201);
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteAsync(Guid orderitemId)
    {
        var orderitem = await _managerRepository.OrderItemRepository.GetOrderItemAsync(orderitemId);
        
        if (orderitem == null) return NotFound();

        await _managerRepository.OrderItemRepository.DeleteOrderItemAsync(orderitem);
        
        return StatusCode(204);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ChangeAsync([FromRoute] Guid id, [FromBody] OrderItemDto orderitemDto)
    {
        var orderitem = await _managerRepository.OrderItemRepository.GetOrderItemAsync(id);
        
        if (orderitem == null) return NotFound();

        await _managerRepository.OrderItemRepository.UpdateOrderItemAsync(orderitem);
        await _managerRepository.SaveChangesRepository.SaveChangesAsync();

        return Ok(orderitem);
    }
}
