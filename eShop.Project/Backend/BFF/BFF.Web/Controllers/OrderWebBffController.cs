namespace BFF.Web.Controllers;

[ApiController]
[Route("bff/orders")]
[Authorize(Policy = "ApiScope")]
public class OrderWebBffController : ControllerBase
{
    private readonly IOrderBffService _orderBffService;

    public OrderWebBffController(IOrderBffService orderBffService)
    {
        _orderBffService = orderBffService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders([FromQuery] int page = 1, [FromQuery] int size = 50)
    {
        var orders = await _orderBffService.GetOrders(page, size);

        return Ok(orders);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderBffService.GetOrderById(id);

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder(OrderRequest orderRequest)
    {

        var addedOrder = await _orderBffService.AddOrder(orderRequest);

        return Ok(addedOrder);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var deletedOrder = await _orderBffService.DeleteOrder(id);

        return Ok(deletedOrder);
    }
}
