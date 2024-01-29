using BFF.Web.Responses;
using BFF.Web.Services.Abstractions;
using Catalog.API.Responses;
using Catalog.Domain.Models;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace BFF.Web.Services;

public class CatalogService : ICatalogService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(IHttpClientFactory httpClientFactory, ILogger<CatalogService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<PaginatedResponse<CatalogBrandResponse>> GetBrands(int page, int size)
    {
        return await GetPaginatedResponse<CatalogBrandResponse>("brands", page, size);
    }

    public async Task<PaginatedResponse<CatalogTypeResponse>> GetTypes(int page, int size)
    {
        return await GetPaginatedResponse<CatalogTypeResponse>("types", page, size);
    }

    public async Task<PaginatedResponse<CatalogItemResponse>> GetItems(int page, int size)
    {
        return await GetPaginatedResponse<CatalogItemResponse>("items", page, size);
    }

    private async Task<PaginatedResponse<T>> GetPaginatedResponse<T>(string endpoint, int page, int size)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                _logger.LogError(disco.Error);
                throw new Exception("Discovery document error");
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "catalog_api_client",
                ClientSecret = "catalog_api_client_secret",
                Scope = "CatalogAPI"
            });

            if (tokenResponse.IsError)
            {
                _logger.LogError(tokenResponse.Error);
                throw new Exception("Token request error");
            }

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync($"http://localhost:5000/api/v1/catalog/{endpoint}?page={page}&size={size}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PaginatedResponse<T>>(content);
                return result;
            }
            else
            {
                _logger.LogError($"WebBff API request failed with status code: {response.StatusCode}");
                throw new Exception("API request error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred: {ex.Message}");
            throw;
        }
    }

}




       
