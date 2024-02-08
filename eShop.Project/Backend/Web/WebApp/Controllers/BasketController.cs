using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services.Abstractions;

namespace WebApp.Controllers;

public class BasketController : Controller
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<IActionResult> Basket()
    {
        var basket = await _basketService.GetBasket(HttpContext);
        return View(basket);
    }

    [HttpPost]
    public async Task<IActionResult> AddBasketItem(ItemRequest itemRequest)
    {
        var userId = _basketService.FindUserId(HttpContext);
        itemRequest.UserId = userId;
        var addedItemId = await _basketService.AddBasketItem(itemRequest);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasketItem(string userId, int itemId)
    {
        userId = _basketService.FindUserId(HttpContext);
        var deletedItemId = await _basketService.DeleteBasketItem(userId, itemId);
        return Ok();
    }
}
