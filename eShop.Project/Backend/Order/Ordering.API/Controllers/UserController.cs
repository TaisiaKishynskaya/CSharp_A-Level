using Microsoft.AspNetCore.Mvc;
using Ordering.Core.Abstractions.Services;
using System.Security.Claims;

namespace Ordering.API.Controllers;

[ApiController]
[Route("api/v1/orders-users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrderUsers([FromQuery] int page, int size) 
    {
        var users = await _userService.Get(page, size);
        return Ok(users);
    }

    [HttpGet("{userId?}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        userId = userId ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userService.GetUserById(userId);
        if (user != null)
        {
            return Ok(user);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("active-user-id")]
    public IActionResult GetActiveUserId()
    {
        var userId = _userService.GetActiveUserId(User);
        return Ok(userId);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser()
    {
        try
        {
            var user = await _userService.Add(User);
            return Ok(user.UserId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
