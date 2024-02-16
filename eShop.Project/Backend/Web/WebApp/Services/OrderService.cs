namespace WebApp.Services;

public class OrderService : IOrderService
{
    private readonly ApiClientSettings _bffClientSettings;
    private readonly IApiClientHelper _apiClientHelper;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        IOptions<MvcApiClientSettings> bffClientSettings,
        IApiClientHelper apiClientHelper,
        ILogger<OrderService> logger)
    {
        _bffClientSettings = bffClientSettings.Value;
        _apiClientHelper = apiClientHelper;
        _logger = logger;
    }

    public async Task<IEnumerable<OrderModel>> GetOrdersByUser(HttpContext httpContext)
    {
        try
        {
            var userId = FindUserId(httpContext);
            _logger.LogInformation($"GetOrdersByUser started. UserId: {userId}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);
            var response = await apiClient.GetAsync($"{_bffClientSettings.ApiUrl}/users/{userId}/orders");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<OrderModel>>(content);
                _logger.LogInformation($"GetOrdersByUser completed. UserId: {userId}");
                return result;
            }
            else
            {
                _logger.LogError($"API request error in GetOrdersByUser. UserId: {userId}");
                throw new Exception("API request error");
            }
        }
        catch (Exception ex)
        {
            var userId = FindUserId(httpContext);
            _logger.LogError(ex, $"Error in GetOrdersByUser. UserId: {userId}");
            throw;
        }
    }

    public async Task<OrderModel> AddOrder(OrderRequest orderRequest)
    {
        try
        {
            _logger.LogInformation($"AddOrder started. OrderRequest: {JsonConvert.SerializeObject(orderRequest)}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);
            var response = await apiClient.PostAsJsonAsync($"{_bffClientSettings.ApiUrl}/orders", orderRequest);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OrderModel>(content);
                _logger.LogInformation($"AddOrder completed. OrderRequest: {JsonConvert.SerializeObject(orderRequest)}");
                return result;
            }
            else
            {
                _logger.LogError($"API request error in AddOrder. OrderRequest: {JsonConvert.SerializeObject(orderRequest)}");
                throw new Exception("API request error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in AddOrder. OrderRequest: {JsonConvert.SerializeObject(orderRequest)}");
            throw;
        }
    }

    public string FindUserId(HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "sub");
        return userIdClaim.Value;
    }
}



