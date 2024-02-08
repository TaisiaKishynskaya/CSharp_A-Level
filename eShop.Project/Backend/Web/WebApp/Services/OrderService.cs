using IdentityModel.Client;
using Newtonsoft.Json;
using WebApp.Models;
using WebApp.Services.Abstractions;

namespace WebApp.Services;

public class OrderService : IOrderService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OrderService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<OrderModel>> GetOrdersByUser(HttpContext httpContext)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var accessToken = await GetAccessToken();
        httpClient.SetBearerToken(accessToken);

        var userId = FindUserId(httpContext);

        var response = await httpClient.GetAsync($"http://localhost:5002/bff/orders/users/{userId}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<IEnumerable<OrderModel>>(content);
        return result;
    }

    public async Task<OrderModel> AddOrder(OrderRequest orderRequest)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var accessToken = await GetAccessToken();
        httpClient.SetBearerToken(accessToken);

        var response = await httpClient.PostAsJsonAsync($"http://localhost:5002/bff/orders", orderRequest);

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<OrderModel>(content);
        return result;
    }

    public string FindUserId(HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "sub");
        return userIdClaim.Value;
    }

    private async Task<string> GetAccessToken()
    {
        var httpClient = _httpClientFactory.CreateClient();

        var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");
        if (disco.IsError)
        {
            throw new Exception("Discovery document error");
        }

        var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "mvc_client",
            ClientSecret = "mvc_secret",
            Scope = "WebBffAPI"
        });

        if (tokenResponse.IsError)
        {
            throw new Exception("Token request error");
        }

        return tokenResponse.AccessToken;
    }
}



