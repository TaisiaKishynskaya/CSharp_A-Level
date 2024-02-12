namespace BFF.Web.Services;

public class OrderBffService : IOrderBffService
{
    private readonly ILogger<OrderBffService> _logger;
    private readonly ApiClientSettings _orderSettings;
    private readonly ApiClientHelper _apiClientHelper;

    public OrderBffService(
        ILogger<OrderBffService> logger,
        IOptions<OrderApiClientSettings> orderSettings,
        ApiClientHelper apiClientHelper
        )
    {
        _logger = logger;
        _orderSettings = orderSettings.Value;
        _apiClientHelper = apiClientHelper;
    }

    public async Task<OrderResponse> GetOrderById(int id)
    {
        try
        {
            _logger.LogInformation($"GetOrderById started. ID: {id}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_orderSettings);
            var response = await apiClient.GetAsync($"{_orderSettings.ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OrderResponse>(content);
                _logger.LogInformation($"GetOrderById completed. ID: {id}");
                return result;
            }

            _logger.LogWarning($"GetOrderById failed. ID: {id}");
            throw new NotFoundException($"Order with ID: {id} was not found.");
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, $"Error in GetOrderById. ID: {id}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetOrderById. ID: {id}");
            throw;
        }
    }

    public async Task<IEnumerable<OrderResponse>> GetOrdersByUser(string userId, int page, int size)
    {
        try
        {
            _logger.LogInformation($"GetOrdersByUser started. UserId: {userId}, Page: {page}, Size: {size}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_orderSettings);
            var response = await apiClient.GetAsync($"http://localhost:5005/api/v1/users/{userId}/orders?page={page}&size={size}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<OrderResponse>>(content);
                _logger.LogInformation($"GetOrdersByUser completed. UserId: {userId}, Page: {page}, Size: {size}");
                return result;
            }

            _logger.LogWarning($"GetOrdersByUser failed. UserId: {userId}, Page: {page}, Size: {size}");
            throw new NotFoundException($"Orders for User ID: {userId} were not found.");
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, $"Error in GetOrdersByUser. UserId: {userId}, Page: {page}, Size: {size}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetOrdersByUser. UserId: {userId}, Page: {page}, Size: {size}");
            throw;
        }
    }

    public async Task<IEnumerable<OrderResponse>> GetOrders(int page, int size)
    {
        try
        {
            _logger.LogInformation($"GetOrders started. Page: {page}, Size: {size}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_orderSettings);
            var response = await apiClient.GetAsync($"{_orderSettings.ApiUrl}?page={page}&size={size}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<OrderResponse>>(content);
                _logger.LogInformation($"GetOrders completed. Page: {page}, Size: {size}");
                return result;
            }

            _logger.LogWarning($"GetOrders failed. Page: {page}, Size: {size}");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetOrders. Page: {page}, Size: {size}");
            throw;
        }
    }

    public async Task<OrderResponse> AddOrder(OrderRequest orderRequest)
    {
        try
        {
            _logger.LogInformation($"AddOrder started. OrderRequest: {JsonConvert.SerializeObject(orderRequest)}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_orderSettings);
            var response = await apiClient.PostAsJsonAsync($"{_orderSettings.ApiUrl}", orderRequest);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OrderResponse>(content);
                _logger.LogInformation($"AddOrder completed. OrderRequest: {JsonConvert.SerializeObject(orderRequest)}");
                return result;
            }

            _logger.LogWarning($"AddOrder failed. OrderRequest: {JsonConvert.SerializeObject(orderRequest)}");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in AddOrder. OrderRequest: {JsonConvert.SerializeObject(orderRequest)}");
            throw;
        }
    }

    public async Task<OrderResponse> DeleteOrder(int id)
    {
        try
        {
            _logger.LogInformation($"DeleteOrder started. ID: {id}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_orderSettings);
            var response = await apiClient.DeleteAsync($"{_orderSettings.ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OrderResponse>(content);
                _logger.LogInformation($"DeleteOrder completed. ID: {id}");
                return result;
            }

            _logger.LogWarning($"DeleteOrder failed. ID: {id}");
            throw new NotFoundException($"Order with ID: {id} was not found.");
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, $"Error in DeleteOrder. ID: {id}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in DeleteOrder. ID: {id}");
            throw;
        }
    }
}
