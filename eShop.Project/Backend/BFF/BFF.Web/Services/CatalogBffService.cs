namespace BFF.Web.Services;

public class CatalogBffService : ICatalogBffService
{
    private readonly ILogger<CatalogBffService> _logger;
    private readonly ApiClientSettings _catalogSettings;
    private readonly IApiClientHelper _apiClientHelper;

    public CatalogBffService(
        ILogger<CatalogBffService> logger,
        IOptions<CatalogApiClientSettings> catalogSettings,
        IApiClientHelper apiClientHelper)
    {
        _logger = logger;
        _catalogSettings = catalogSettings.Value;
        _apiClientHelper = apiClientHelper;
    }

    public async Task<PaginatedResponse<CatalogBrandResponse>> GetBrands(int page, int size)
    {
        try
        {
            _logger.LogInformation($"GetBrands started. Page: {page}, Size: {size}");
            var result = await GetPaginatedResponse<CatalogBrandResponse>("brands", page, size);
            _logger.LogInformation($"GetBrands completed. Page: {page}, Size: {size}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetBrands. Page: {page}, Size: {size}");
            throw;
        }
    }

    public async Task<PaginatedResponse<CatalogTypeResponse>> GetTypes(int page, int size)
    {
        try
        {
            _logger.LogInformation($"GetTypes started. Page: {page}, Size: {size}");
            var result = await GetPaginatedResponse<CatalogTypeResponse>("types", page, size);
            _logger.LogInformation($"GetTypes completed. Page: {page}, Size: {size}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetTypes. Page: {page}, Size: {size}");
            throw;
        }
    }

    public async Task<PaginatedResponse<CatalogItemResponse>> GetItems(int page, int size)
    {
        try
        {
            _logger.LogInformation($"GetItems started. Page: {page}, Size: {size}");
            var result = await GetPaginatedResponse<CatalogItemResponse>("items", page, size);
            _logger.LogInformation($"GetItems completed. Page: {page}, Size: {size}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetItems. Page: {page}, Size: {size}");
            throw;
        }
    }

    public async Task<CatalogItemResponse> GetItemById(int id)
    {
        try
        {
            _logger.LogInformation($"GetItemById started. ID: {id}");
            var result = await GetUnitById<CatalogItemResponse>("items", id);

            _logger.LogInformation($"GetItemById completed. ID: {id}");
            return result;
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, $"Error in GetItemById. ID: {id}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetItemById. ID: {id}");
            throw;
        }
    }

    private async Task<PaginatedResponse<T>> GetPaginatedResponse<T>(string endpoint, int page, int size)
    {
        try
        {
            _logger.LogInformation($"GetPaginatedResponse started. Endpoint: {endpoint}, Page: {page}, Size: {size}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_catalogSettings);
            var response = await apiClient.GetAsync($"{_catalogSettings.ApiUrl}/{endpoint}?page={page}&size={size}");

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaginatedResponse<T>>(content);
            _logger.LogInformation($"GetPaginatedResponse completed. Endpoint: {endpoint}, Page: {page}, Size: {size}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetPaginatedResponse. Endpoint: {endpoint}, Page: {page}, Size: {size}");
            throw;
        }
    }

    private async Task<T> GetUnitById<T>(string endpoint, int id)
    {

        _logger.LogInformation($"GetUnitById started. Endpoint: {endpoint}, ID: {id}");
        var apiClient = await _apiClientHelper.CreateClientWithToken(_catalogSettings);
        var response = await apiClient.GetAsync($"{_catalogSettings.ApiUrl}/{endpoint}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);
            _logger.LogInformation($"GetUnitById completed. Endpoint: {endpoint}, ID: {id}");
            return result;
        }
        throw new NotFoundException($"Item with ID: {id} was not found.");
    }
}