namespace Basket.Application.Services;

public class BasketService : IBasketService
{
    private readonly ICatalogService _catalogService;
    private readonly ICacheService _cacheService;
    private readonly ILogger<BasketService> _logger;

    public BasketService(
        ICatalogService catalogService,
        ICacheService cacheService,
        ILogger<BasketService> logger)
    {
        _catalogService = catalogService;
        _cacheService = cacheService;
        _logger = logger;

    }

    public async Task<Domain.Models.Basket> GetBasket(string userId)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError($"Error: User ID cannot be null or empty, Stack Trace: {Environment.StackTrace}");
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
            }

            var startTime = DateTime.UtcNow;
            var data = await _cacheService.Get(userId);
            var endTime = DateTime.UtcNow;

            if (data == null)
            {
                _logger.LogInformation($"Operation: CreateBasket, UserId: {userId}, Start Time: {startTime}, End Time: {endTime}, Status: Success");
                return await CreateBasket(userId);
            }
            _logger.LogInformation($"Basket found for user: {userId}");
            return JsonConvert.DeserializeObject<Domain.Models.Basket>(data);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<BasketItem> AddItem(string userId, int itemId)
    {
        try
        {
            var item = await _catalogService.GetItemById(itemId);
            if (item == null)
            {
                _logger.LogError($"Error: Item with id = {itemId} not found, Stack Trace: {Environment.StackTrace}");
                throw new ArgumentException($"Item with id = {itemId} not found");
            }

            var basket = await GetBasket(userId);

            var basketItem = basket.Items.FirstOrDefault(i => i.ItemId == item.Id);
            if (basketItem != null)
            {
                basketItem.Quantity++;
            }
            else
            {
                basketItem = new BasketItem
                {
                    ItemId = item.Id,
                    ItemTitle = item.Title,
                    ItemPrice = item.Price,
                    PictureUrl = item.PictureFile,
                    Quantity = 1
                };
                basket.Items.Add(basketItem);
            }

            await UpdateBasket(basket);
            return basketItem;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<BasketItem> RemoveItem(string userId, int id)
    {
        try
        {
            var basket = await GetBasket(userId);

            var basketItem = basket.Items.FirstOrDefault(i => i.ItemId == id);
            if (basketItem == null)
            {
                _logger.LogError($"Error: Item with id = {id} not found in the basket, Stack Trace: {Environment.StackTrace}");
                throw new ArgumentException($"Item with id = {id} not found in the basket");
            }

            if (basketItem.Quantity > 1)
            {
                basketItem.Quantity--;
            }
            else
            {
                basket.Items.Remove(basketItem);
            }

            if (!basket.Items.Any())
            {
                await DeleteBasket(userId);
            }
            else
            {
                await UpdateBasket(basket);
            }

            return basketItem;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<bool> DeleteBasket(string userId)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError($"Error: User ID cannot be null or empty, Stack Trace: {Environment.StackTrace}");
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
            }

            var result = await _cacheService.Delete(userId);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    private async Task<Domain.Models.Basket> CreateBasket(string userId)
    {
        Domain.Models.Basket newBasket = new Domain.Models.Basket
        {
            UserId = userId,
        };

        await UpdateBasket(newBasket);
        return newBasket;
    }

    private async Task<Domain.Models.Basket> UpdateBasket(Domain.Models.Basket basket)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var createdBasket = await _cacheService.Set(basket.UserId, JsonConvert.SerializeObject(basket));
            var endTime = DateTime.UtcNow;

            _logger.LogInformation($"Operation: UpdateBasket, UserId: {basket.UserId}, Start Time: {startTime}, End Time: {endTime}, Status: {(createdBasket ? "Success" : "Failure")}");
            return await GetBasket(basket.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

}



