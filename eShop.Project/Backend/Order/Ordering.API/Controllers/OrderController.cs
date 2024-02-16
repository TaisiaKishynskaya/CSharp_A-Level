namespace Ordering.API.Controllers;

[ApiController]
[Route("api/v1/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public OrderController(
        IOrderService orderService,
        IMapper mapper,
        IUserService userService)
    {
        _orderService = orderService;
        _mapper = mapper;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders([FromQuery] int page = 1, int size = 50)
    {
        try
        {
            var orders = await _orderService.Get(page, size);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        try
        {
            var order = await _orderService.GeyById(id);
            return Ok(order);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder(OrderRequest orderRequest)
    {
        try
        {
            var order = new Order { Address = orderRequest.Address };
            //var addedUser = await _userService.GetOrCreate(User);

            var createdOrder = await _orderService.Add(order, orderRequest.UserId);
            return Ok(createdOrder);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderUpdateRequest orderUpdateRequest)
    {
        try
        {
            var order = _mapper.Map<Order>(orderUpdateRequest);
            order.Id = id;

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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        try
        {
            var order = await _orderService.Delete(id);
            return Ok(order);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
