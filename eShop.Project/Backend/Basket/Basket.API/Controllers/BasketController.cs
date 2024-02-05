using Basket.API.Services;
using Basket.Core.Abstractions;
using Basket.Domain.Models;
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
        //var userId = _userService.GetUserId(User);
        var basket = await _basketService.GetBasket(userId);

        if (basket == null)
        {
            return BadRequest("Basket is empty");
        }

        return Ok(basket);
    }


    [HttpPost]
    public async Task<IActionResult> AddItem([FromBody] ItemRequest itemRequest)
    {
        try
        {
            var userId = itemRequest.UserId; 
            var createdItem = await _basketService.AddItem(userId, itemRequest);
            return Ok(itemRequest.ItemId);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{userId}/{itemId}")]
    public async Task<IActionResult> DeleteItem(string userId, int itemId)
    {
        try
        {
            //userId = _userService.GetUserId(User);
            var deletedItem = await _basketService.RemoveItem(userId, itemId);
            return Ok(deletedItem.ItemId);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteBasket(string userId)
    {
        try
        {
            var deleted = await _basketService.DeleteBasket(userId);
            if (deleted)
            {
                return Ok(userId);
            }
            else
            {
                return NotFound($"Basket for user {userId} not found.");
            }
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }


}
