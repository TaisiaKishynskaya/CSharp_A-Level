using Helpers.Abstractions;

namespace Basket.Application.Services;

public class CatalogService : ICatalogService
{
    private readonly ApiClientSettings _catalogSettigns;
    private readonly IApiClientHelper _apiClientHelper;
    private readonly ILogger<CatalogService> _logger;
    public CatalogService(
        IOptions<ApiClientSettings> catalogSettigns,
        IApiClientHelper apiClientHelper,
        ILogger<CatalogService> logger)
    {
        _catalogSettigns = catalogSettigns.Value;
        _apiClientHelper = apiClientHelper;
        _logger = logger;

    }

    public async Task<CatalogItem> GetItemById(int id)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var itemResponse = await GetCatalogItemById(id);
            var endTime = DateTime.UtcNow;

            if (itemResponse == null || itemResponse.Id == 0)
            {
                _logger.LogError($"Error: Item with id = {id} not found, Stack Trace: {Environment.StackTrace}");
                throw new ArgumentException($"Item with id = {id} not found");
            }

            _logger.LogInformation($"Operation: GetItemById, Id: {id}, Start Time: {startTime}, End Time: {endTime}, Status: Success");
            return itemResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    private async Task<CatalogItem> GetCatalogItemById(int id)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var apiClient = await _apiClientHelper.CreateClientWithToken(_catalogSettigns);

            var response = await apiClient.GetAsync($"{_catalogSettigns.ApiUrl}/items/{id}");

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CatalogItem>(content);
            var endTime = DateTime.UtcNow;

            _logger.LogInformation($"Operation: GetCatalogItemById, Id: {id}, Start Time: {startTime}, End Time: {endTime}, Status: {(result != null ? "Success" : "Failure")}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }
}
