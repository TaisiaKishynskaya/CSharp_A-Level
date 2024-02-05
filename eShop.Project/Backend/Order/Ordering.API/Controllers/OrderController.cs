using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Requests;
using Ordering.Core.Abstractions.Services;
using Ordering.Domain.Models;
using System.Security.Claims;

namespace Ordering.API.Controllers;

[ApiController]
[Route("api/v1/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderController(
        IOrderService orderService,
        IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders([FromQuery] int page = 1, int size = 50)
    {
        var orders = await _orderService.Get(page, size);
        return Ok(orders);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetOrdersByActiveUser([FromQuery] int page = 1, [FromQuery] int size = 50)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var orders = await _orderService.GetByUser(userId, page, size);
        return Ok(orders);
    }

    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetOrdersByUser(string userId, [FromQuery] int page = 1, [FromQuery] int size = 50)
    {
        var orders = await _orderService.GetByUser(userId, page, size);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GeyById(id);
        if (order != null)
        {
            return Ok(order);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder(OrderRequest orderRequest)
    {
        var order = new Order { Address = orderRequest.Address };
        var createdOrder = await _orderService.Add(order, orderRequest.UserId);
        return Ok(createdOrder);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderUpdateRequest orderUpdateRequest)
    {
        var order = _mapper.Map<Order>(orderUpdateRequest);
        order.Id = id;

        try
        {
            var updatedOrder = await _orderService.Update(order, User);
            return Ok(updatedOrder);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Order not found");
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid("You do not have permission to update this order");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _orderService.Delete(id);

        if (order == null)
        {
            return NotFound("Order not found");
        }

        return Ok(order);
    }
}
