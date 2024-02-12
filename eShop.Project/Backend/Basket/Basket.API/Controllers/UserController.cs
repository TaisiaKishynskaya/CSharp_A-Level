using Helpers.Extensions;

namespace Basket.API.Controllers;

[ApiController]
[Route("/api/v1/user")]
public class UserController : ControllerBase
{
	public UserController()
	{
	}

    [HttpGet]
    public IActionResult GetActiveUser()
    {
        var userId = User.GetUserId();
        return Ok(userId);
    }
}
