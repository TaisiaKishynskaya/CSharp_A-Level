namespace BFF.Web.Services;

public class BasketBffService : IBasketBffService
{
    private readonly ILogger<BasketBffService> _logger;
    private readonly ApiClientSettings _basketSettings;
    private readonly ApiClientHelper _apiClientHelper;

    public BasketBffService(
        ILogger<BasketBffService> logger,
        IOptions<BasketApiClientSettings> basketSettings,
        ApiClientHelper apiClientHelper)
    {
        _logger = logger;
        _basketSettings = basketSettings.Value;
        _apiClientHelper = apiClientHelper;
    }

    public async Task<BasketResponse> GetBasket(string userId)
    {
        try
        {
            _logger.LogInformation($"GetBasket started. UserId: {userId}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_basketSettings);
            var response = await apiClient.GetAsync($"{_basketSettings.ApiUrl}/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BasketResponse>(content);
                _logger.LogInformation($"GetBasket completed. UserId: {userId}");
                return result;
            }

            _logger.LogWarning($"GetBasket failed. UserId: {userId}");
            throw new NotFoundException($"Basket for User ID: {userId} was not found.");
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, $"Error in GetBasket. UserId: {userId}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetBasket. UserId: {userId}");
            throw;
        }
    }

    public async Task<int> AddBasketItem(ItemRequest itemRequest)
    {
        try
        {
            _logger.LogInformation($"AddBasketItem started. ItemRequest: {JsonConvert.SerializeObject(itemRequest)}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_basketSettings);
            var response = await apiClient.PostAsJsonAsync($"{_basketSettings.ApiUrl}", itemRequest);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<int>(responseString);
                _logger.LogInformation($"AddBasketItem completed. ItemRequest: {JsonConvert.SerializeObject(itemRequest)}");
                return result;
            }

            _logger.LogWarning($"AddBasketItem failed. ItemRequest: {JsonConvert.SerializeObject(itemRequest)}");
            return -1;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in AddBasketItem. ItemRequest: {JsonConvert.SerializeObject(itemRequest)}");
            throw;
        }
    }

    public async Task<DeleteBasketResponse> DeleteBasketItem(string userId, int itemId)
    {
        try
        {
            _logger.LogInformation($"DeleteBasketItem started. UserId: {userId}, ItemId: {itemId}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_basketSettings);
            var response = await apiClient.DeleteAsync($"{_basketSettings.ApiUrl}/{userId}?itemId={itemId}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<DeleteBasketResponse>(responseString);
                _logger.LogInformation($"DeleteBasketItem completed. UserId: {userId}, ItemId: {itemId}");
                return result;
            }

            _logger.LogWarning($"DeleteBasketItem failed. UserId: {userId}, ItemId: {itemId}");
            throw new NotFoundException($"Basket Item with ID: {itemId} for User ID: {userId} was not found.");
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, $"Error in DeleteBasketItem. UserId: {userId}, ItemId: {itemId}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in DeleteBasketItem. UserId: {userId}, ItemId: {itemId}");
            throw;
        }
    }
}