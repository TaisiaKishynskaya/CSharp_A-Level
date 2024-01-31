using Basket.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("/api/v1/basket")]
public class BasketController : ControllerBase
{
    private readonly ICacheService _cacheService;
    private readonly IBasketService _basketService;
    private readonly ICatalogService _catalogService;
    private readonly IUserService _userService;

    public BasketController(
        ICacheService cacheService,
        IBasketService basketService,
        ICatalogService catalogService,
        IUserService userService)
    {
        _cacheService = cacheService;
        _basketService = basketService;
        _catalogService = catalogService;
        _userService = userService;

    }

    [HttpGet("id")]
    public async Task<IActionResult> GetUserId()
    {
        var userId = _userService.GetUserId(User);
        return Ok(userId);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetBasket(string userId)
    {
        var basket = await _basketService.GetBasket(userId);

        if (basket == null)
        {
            return BadRequest("Basket is empty");
        }

        return Ok(basket);
    }

    [HttpPost]
    public async Task<IActionResult> AddItem([FromBody] int itemId)
    {
        try
        {
            var userId = _userService.GetUserId(User);
            var createdItem = await _basketService.AddItem(userId, itemId);
            return Ok($"Item with id = {itemId} was successfully added to basket");
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{itemId}")]
    public async Task<IActionResult> DeleteItem(int itemId)
    {
        try
        {
            var userId = _userService.GetUserId(User);
            var deletedItem = await _basketService.RemoveItem(userId, itemId);
            return Ok($"Item with id = {itemId} was successfully deleted from basket");
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }


}
