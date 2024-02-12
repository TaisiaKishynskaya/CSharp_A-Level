namespace Ordering.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IOrderService _orderService;

    public UserController(
        IUserService userService,
        IOrderService orderService)
    {
        _userService = userService;
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsersWithAtLeastOneOrder([FromQuery] int page, int size)
    {

        var users = await _userService.Get(page, size);
        return Ok(users);
    }

    [HttpGet("{userId}/orders")]
    public async Task<IActionResult> GetOrdersByUser(string userId, [FromQuery] int page = 1, [FromQuery] int size = 50)
    {

        var orders = await _orderService.GetByUser(userId, page, size);
        return Ok(orders);
    }

    [HttpGet("{userId?}/details")]
    public async Task<IActionResult> GetUserById(string userId)
    {

        userId = userId ?? User.GetUserId();
        var user = await _userService.GetUserById(userId);
        return Ok(user);
    }

    [HttpGet("me")]
    public IActionResult GetActiveUserId()
    {

        var userId = _userService.GetActiveUserId(User);
        return Ok(userId);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser()
    {
        var user = await _userService.Add(User);
        return Ok(user.UserId);
    }
}