using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Services;
using System.Security.Claims;
using IdentityModel;

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
    public async Task<IActionResult> GetAllOrderUsers(int page, int size) 
    {
        var users = await _userService.Get(page, size);
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser()
    {
        try
        {
            //var name = User.FindFirst(JwtClaimTypes.Name)?.Value;
            //var email = User.FindFirst(JwtClaimTypes.Email)?.Value;
            var user = await _userService.Add(User);
            return Ok(user.UserId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}
