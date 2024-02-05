using BFF.Web.Responses;
using BFF.Web.Services.Abstractions;

namespace BFF.Web.Controllers;

[ApiController]
[Route("bff")]
[Authorize(Policy = "ApiScope")]
public class OrderWebBffController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderWebBffController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("orders/{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderById(id);

        if (order == null) 
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpGet("orders/users/{userId}")]
    public async Task<IActionResult> GetOrdersByUser(string userId, [FromQuery] int page = 1, [FromQuery] int size = 50)
    {
        var orders = await _orderService.GetOrdersByUser(userId, page, size);

        if (orders == null)
        {
            return NotFound();
        }

        return Ok(orders);
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetAllOrders([FromQuery] int page = 1, [FromQuery] int size = 50)
    {
        var orders = await _orderService.GetOrders(page, size);

        if (orders == null)
        {
            return NotFound();
        }

        return Ok(orders);
    }

    [HttpPost("orders")]
    public async Task<IActionResult> AddOrder(OrderRequest orderRequest)
    {
        try
        {
            var addedOrder = await _orderService.AddOrder(orderRequest);

            return Ok(addedOrder);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("orders/{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {

        var deletedOrder = await _orderService.DeleteOrder(id);

        if (deletedOrder == null)
        {
            return NotFound();
        }

        return Ok(deletedOrder);
        
    }
}
