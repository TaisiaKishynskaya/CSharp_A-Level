using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;
using WebApp.Models;
using WebApp.Services.Abstractions;

namespace WebApp.Services;

public class BasketService : IBasketService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BasketService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<BasketModel> GetBasket(HttpContext httpContext)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var accessToken = await GetAccessToken();
        httpClient.SetBearerToken(accessToken);

        var userId = FindUserId(httpContext);

        var response = await httpClient.GetAsync($"http://localhost:5002/bff/basket/{userId}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BasketModel>(content);
        return result;
    }

    public async Task<int> AddBasketItem(ItemRequest itemRequest)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var accessToken = await GetAccessToken();
        httpClient.SetBearerToken(accessToken);

        var response = await httpClient.PostAsJsonAsync($"http://localhost:5002/bff/basket", itemRequest);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<int>(responseString);
        return result;
    }

    public async Task<int> DeleteBasketItem(string userId, int itemId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var accessToken = await GetAccessToken();
        httpClient.SetBearerToken(accessToken);

        var response = await httpClient.DeleteAsync($"http://localhost:5002/bff/basket/{userId}/{itemId}");

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<int>(responseString);
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
