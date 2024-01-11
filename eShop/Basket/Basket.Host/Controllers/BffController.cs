using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Basket.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "AnyScope")]
    public class BffController : ControllerBase
    {
        [HttpGet("/message")]
        [AllowAnonymous]
        public IActionResult GetTestMessage()
        {
            return Ok("TestMessage");
        }

        [HttpGet("/user-id")]
        public IActionResult GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(userId);
        }
    }
}
