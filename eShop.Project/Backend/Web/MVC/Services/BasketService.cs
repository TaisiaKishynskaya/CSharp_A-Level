namespace WebApp.Services;

public class BasketService : IBasketService
{
    private readonly ApiClientSettings _bffClientSettings;
    private readonly ApiClientHelper _apiClientHelper;
    private readonly ILogger<BasketService> _logger;

    public BasketService(
        IOptions<MvcApiClientSettings> bffClientSettings,
        ApiClientHelper apiClientHelper,
        ILogger<BasketService> logger)
    {
        _bffClientSettings = bffClientSettings.Value;
        _apiClientHelper = apiClientHelper;
        _logger = logger;
    }

    public async Task<BasketModel> GetBasket(HttpContext httpContext)
    {
        try
        {
            var userId = FindUserId(httpContext);
            _logger.LogInformation($"GetBasket started. UserId: {userId}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);
            var response = await apiClient.GetAsync($"{_bffClientSettings.ApiUrl}/basket/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BasketModel>(content);
                _logger.LogInformation($"GetBasket completed. UserId: {userId}");
                return result;
            }
            else
            {
                _logger.LogError($"API request error in GetBasket. UserId: {userId}");
                throw new Exception("API request error");
            }
        }
        catch (Exception ex)
        {
            var userId = FindUserId(httpContext);
            _logger.LogError(ex, $"Error in GetBasket. UserId: {userId}");
            throw;
        }
    }

    public async Task<int> AddBasketItem(ItemRequest itemRequest)
    {
        try
        {
            _logger.LogInformation($"AddBasketItem started. ItemRequest: {JsonConvert.SerializeObject(itemRequest)}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);
            var response = await apiClient.PostAsJsonAsync($"{_bffClientSettings.ApiUrl}/basket", itemRequest);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<int>(responseString);
                _logger.LogInformation($"AddBasketItem completed. ItemRequest: {JsonConvert.SerializeObject(itemRequest)}");
                return result;
            }
            else
            {
                _logger.LogError($"API request error in AddBasketItem. ItemRequest: {JsonConvert.SerializeObject(itemRequest)}");
                throw new Exception("API request error");
            }
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
            var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);
            var response = await apiClient.DeleteAsync($"{_bffClientSettings.ApiUrl}/basket/{userId}?itemId={itemId}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<DeleteBasketResponse>(responseString);
                _logger.LogInformation($"DeleteBasketItem completed. UserId: {userId}, ItemId: {itemId}");
                return result;
            }
            else
            {
                _logger.LogError($"API request error in DeleteBasketItem. UserId: {userId}, ItemId: {itemId}");
                throw new Exception("API request error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in DeleteBasketItem. UserId: {userId}, ItemId: {itemId}");
            throw;
        }
    }

    public string FindUserId(HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "sub");
        return userIdClaim.Value;
    }
}
