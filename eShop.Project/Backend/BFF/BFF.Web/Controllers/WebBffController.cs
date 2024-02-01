using BFF.Web.Services;
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


    // catalog
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

    //user
    [HttpGet("user/id")]
    public async Task<IActionResult> GetUserId()
    {
        var userId = _userService.GetUserId(User);
        if (userId == null)
            return Ok("User is null");
        else
            return Ok(userId);
    }

    //basket
    //with id query
    [HttpGet("basket/{userId}")]
    public async Task<IActionResult> GetBasket(string userId)
    {
        //userId = _userService.GetUserId(User);
        var basket = await _basketService.GetBasket(userId);
        return Ok(basket);
    }


    [HttpPost("basket")]
    public async Task<IActionResult> AddItem([FromBody] ItemRequest itemRequest)
    {
        itemRequest.UserId = _userService.GetUserId(User);
        var addedItem = await _basketService.AddBasketItem(itemRequest);
        return Ok(addedItem);
    }

    [HttpDelete("basket/{userId}/{itemId}")]
    public async Task<IActionResult> DeleteItem(string userId, int itemId)
    {
        var deletedItemId = await _basketService.DeleteBasketItem(userId, itemId);
        return Ok(deletedItemId);
    }

    //without id
    //with id query

    //[HttpGet("basket")]
    //public async Task<IActionResult> GetBasket()
    //{
    //    var userId = _userService.GetUserId(User);
    //    var basket = await _basketService.GetBasket(userId);
    //    return Ok(basket);
    //}

    //[HttpDelete("basket/{userId}/{itemId}")]
    //public async Task<IActionResult> DeleteItem(int itemId)
    //{
    //    var userId = _userService.GetUserId(User);
    //    var deletedItemId = await _basketService.DeleteBasketItem(userId, itemId);
    //    return Ok(deletedItemId);
    //}

}