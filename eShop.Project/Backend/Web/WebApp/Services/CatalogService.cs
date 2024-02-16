namespace WebApp.Services;

public class CatalogService : ICatalogService
{
    private readonly ApiClientSettings _bffClientSettings;
    private readonly IApiClientHelper _apiClientHelper;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(
        IOptions<MvcApiClientSettings> bffClientSettings,
        IApiClientHelper apiClientHelper,
        ILogger<CatalogService> logger      
        )
    {
        _bffClientSettings = bffClientSettings.Value;
        _apiClientHelper = apiClientHelper;
        _logger = logger;
    }

    public async Task<PaginatedDataModel<CatalogItemModel>> GetCatalogItems(int page, int size, string sort, List<int> types = null, List<int> brands = null)
    {
        try
        {
            _logger.LogInformation($"GetCatalogItems started. Page: {page}, Size: {size}, Sort: {sort}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);
            var response = await apiClient.GetAsync($"{_bffClientSettings.ApiUrl}/catalog/items?page={page}&size={size}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PaginatedDataModel<CatalogItemModel>>(content);

                if (types != null && types.Any())
                {
                    result.Data = result.Data.Where(item => types.Contains(item.Type.Id)).ToList();
                }
                if (brands != null && brands.Any())
                {
                    result.Data = result.Data.Where(item => brands.Contains(item.Brand.Id)).ToList();
                }

                switch (sort)
                {
                    case "price_asc":
                        result.Data = result.Data.OrderBy(item => item.Price).ToList();
                        break;
                    case "price_desc":
                        result.Data = result.Data.OrderByDescending(item => item.Price).ToList();
                        break;
                    case "name_asc":
                        result.Data = result.Data.OrderBy(item => item.Title).ToList();
                        break;
                    case "name_desc":
                        result.Data = result.Data.OrderByDescending(item => item.Title).ToList();
                        break;
                    case "date_asc":
                        result.Data = result.Data.OrderBy(item => item.CreatedAt).ToList();
                        break;
                    case "date_desc":
                        result.Data = result.Data.OrderByDescending(item => item.CreatedAt).ToList();
                        break;
                }

                _logger.LogInformation($"GetCatalogItems completed. Page: {page}, Size: {size}, Sort: {sort}");
                return result;
            }
            else
            {
                _logger.LogError($"API request error in GetCatalogItems. Page: {page}, Size: {size}, Sort: {sort}");
                throw new Exception("API request error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetCatalogItems. Page: {page}, Size: {size}, Sort: {sort}");
            throw;
        }
    }

    public async Task<PaginatedDataModel<CatalogTypeModel>> GetCatalogTypes()
    {
        try
        {
            _logger.LogInformation("GetCatalogTypes started.");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);
            var response = await apiClient.GetAsync($"{_bffClientSettings.ApiUrl}/catalog/types");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PaginatedDataModel<CatalogTypeModel>>(content);
                _logger.LogInformation("GetCatalogTypes completed.");
                return result;
            }
            else
            {
                _logger.LogError("API request error in GetCatalogTypes.");
                throw new Exception("API request error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetCatalogTypes.");
            throw;
        }
    }

    public async Task<PaginatedDataModel<CatalogBrandModel>> GetCatalogBrands()
    {
        try
        {
            _logger.LogInformation("GetCatalogBrands started.");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);
            var response = await apiClient.GetAsync($"{_bffClientSettings.ApiUrl}/catalog/brands");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PaginatedDataModel<CatalogBrandModel>>(content);
                _logger.LogInformation("GetCatalogBrands completed.");
                return result;
            }
            else
            {
                _logger.LogError("API request error in GetCatalogBrands.");
                throw new Exception("API request error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetCatalogBrands.");
            throw;
        }
    }

    public async Task<CatalogItemModel> GetCatalogItemById(int id)
    {
        try
        {
            _logger.LogInformation($"GetCatalogItemById started. ID: {id}");
            var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);
            var response = await apiClient.GetAsync($"{_bffClientSettings.ApiUrl}/catalog/items/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CatalogItemModel>(content);
                _logger.LogInformation($"GetCatalogItemById completed. ID: {id}");
                return result;
            }
            else
            {
                _logger.LogError($"API request error in GetCatalogItemById. ID: {id}");
                throw new Exception("API request error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetCatalogItemById. ID: {id}");
            throw;
        }
    }
}
