using BFF.Web.Responses;
using BFF.Web.Services.Abstractions;
using IdentityModel.Client;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BFF.Web.Services;

public class BasketService : IBasketService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BasketService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Basket> GetBasket(string userId)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");


        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "basket_api_client",
            ClientSecret = "basket_api_client_secret",
            Scope = "BasketAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);
;
        var response = await apiClient.GetAsync($"http://localhost:5004/api/v1/basket/{userId}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Basket>(content);
        return result;
    }

    public async Task<int> AddBasketItem(ItemRequest itemRequest)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "basket_api_client",
            ClientSecret = "basket_api_client_secret",
            Scope = "BasketAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.PostAsJsonAsync($"http://localhost:5004/api/v1/basket", itemRequest);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to add item: {response.ReasonPhrase}");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<int>(responseString);
        return result;
    }

    public async Task<int> DeleteBasketItem(string userId, int itemId)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "basket_api_client",
            ClientSecret = "basket_api_client_secret",
            Scope = "BasketAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.DeleteAsync($"http://localhost:5004/api/v1/basket/{userId}/{itemId}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to delete item: {response.ReasonPhrase}");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<int>(responseString);
        return result;
    }

}

public class ItemRequest
{
    public string UserId { get; set; }
    public int ItemId { get; set; }
}
