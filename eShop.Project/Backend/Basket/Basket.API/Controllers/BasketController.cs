namespace Basket.API.Controllers;

[ApiController]
[Route("/api/v1/basket")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
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
    public async Task<IActionResult> AddItem([FromBody] ItemRequest itemRequest)
    {
        var userId = itemRequest.UserId;
        var createdItem = await _basketService.AddItem(userId, itemRequest.ItemId);
        return Ok(itemRequest.ItemId);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteBasketOrItem(string userId, [FromQuery] int itemId)
    {
        if (itemId == 0)
        {
            var deleted = await _basketService.DeleteBasket(userId);
            if (deleted)
            {
                return Ok(new DeleteResponse { Type = "Basket", Id = userId });
            }
            else
            {
                return NotFound($"Basket for user {userId} not found.");
            }
        }
        else
        {
            var deletedItem = await _basketService.RemoveItem(userId, itemId);
            return Ok(new DeleteResponse { Type = "Item", Id = deletedItem.ItemId.ToString() });
        }
    }
}