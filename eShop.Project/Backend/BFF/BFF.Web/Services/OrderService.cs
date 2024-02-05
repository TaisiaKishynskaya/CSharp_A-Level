using BFF.Web.Responses;
using BFF.Web.Services.Abstractions;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace BFF.Web.Services;

public class OrderService : IOrderService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OrderService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Order> GetOrderById(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "order_api_client",
            ClientSecret = "order_api_client_secret",
            Scope = "OrderAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.GetAsync($"http://localhost:5005/api/v1/orders/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Order>(content);
            return result;
        }

        return null;
    }

    public async Task<IEnumerable<Order>> GetOrdersByUser(string userId, int page, int size)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "order_api_client",
            ClientSecret = "order_api_client_secret",
            Scope = "OrderAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.GetAsync($"http://localhost:5005/api/v1/orders/users/{userId}?page={page}&size={size}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Order>>(content);
            return result;
        }

        return null;
    }

    public async Task<IEnumerable<Order>> GetOrders(int page, int size)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "order_api_client",
            ClientSecret = "order_api_client_secret",
            Scope = "OrderAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.GetAsync($"http://localhost:5005/api/v1/orders?page={page}&size={size}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Order>>(content);
            return result;
        }

        return null;
    }

    public async Task<Order> AddOrder(OrderRequest orderRequest)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "order_api_client",
            ClientSecret = "order_api_client_secret",
            Scope = "OrderAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.PostAsJsonAsync($"http://localhost:5005/api/v1/orders", orderRequest);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Order>(content);
            return result;
        }

        return null;
    }

    public async Task<Order> DeleteOrder(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "order_api_client",
            ClientSecret = "order_api_client_secret",
            Scope = "OrderAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.DeleteAsync($"http://localhost:5005/api/v1/orders/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Order>(content);
            return result;
        }

        return null;
    }
}
