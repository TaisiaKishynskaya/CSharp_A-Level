namespace BFF.Web.Controllers;

[ApiController]
[Route("/bff/basket")]
[Authorize(Policy = "ApiScope")]
public class BasketBffController : ControllerBase
{
    private readonly IBasketBffService _basketBffService;

    public BasketBffController(IBasketBffService basketBffService)
    {
        _basketBffService = basketBffService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        throw new NotFoundException("This is a test exception.");
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetBasket(string userId)
    {
        var basket = await _basketBffService.GetBasket(userId);
        return Ok(basket);
    }


    [HttpPost]
    public async Task<IActionResult> AddItem([FromBody] ItemRequest itemRequest)
    {
        var addedItemId = await _basketBffService.AddBasketItem(itemRequest);
        return Ok(addedItemId);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteItem(string userId, [FromQuery] int itemId)
    {
        var deletedItemId = await _basketBffService.DeleteBasketItem(userId, itemId);
        return Ok(deletedItemId);
    }
}
