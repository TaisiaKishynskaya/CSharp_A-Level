using BFF.Web.Services.Abstractions;
using System.Security.Claims;

namespace BFF.Web.Controllers;

[ApiController]
[Route("/bff")]
[Authorize(Policy = "ApiScope")]
public class WebBffController : ControllerBase
{
    private readonly ICatalogService _catalogService;
    private readonly IUserService _userService;
    private readonly IBasketService _basketService;

    public WebBffController(
        ICatalogService catalogService,
        IUserService userService,
        IBasketService basketService)
    {
        _catalogService = catalogService;
        _userService = userService;
        _basketService = basketService;

    }

    [HttpGet("catalog/brands")]
    public async Task<IActionResult> GetBrands(int page = 1, int size = 3)
    {
        try
        {
            var brands = await _catalogService.GetBrands(page, size);
            return Ok(brands);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("catalog/types")]
    public async Task<IActionResult> GetTypes(int page = 1, int size = 3)
    {
        try
        {
            var types = await _catalogService.GetTypes(page, size);
            return Ok(types);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("catalog/items")]
    public async Task<IActionResult> GetItems(int page = 1, int size = 10)
    {
        try
        {
            var items = await _catalogService.GetItems(page, size);
            return Ok(items);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("catalog/items/{id}")]
    public async Task<IActionResult> GetItemById(int id)
    {
        var item = await _catalogService.GetItemById(id);
        return Ok(item);
    }

    [HttpGet("basket")]
    public async Task<IActionResult> GetBasket()
    {
        var userId = _userService.GetUserId(User);
        var basket = await _basketService.GetBasket(userId);
        return Ok(basket);
    }

    [HttpGet("user/id")]
    public async Task<IActionResult> GetUserId()
    {
        var userId = _userService.GetUserId(User);
        if (userId == null)
            return Ok("User is null");
        else
            return Ok(userId);
    }

    //[HttpPost("basket/item")]
    //public async Task<IActionResult> AddItem([FromBody] int itemId)
    //{
    //        var userId = _userService.GetUserId(User);
    //        var createdItem = await _basketService.AddBasketItem(userId, itemId);
    //        return Ok(createdItem);
    //}


}