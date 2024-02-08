using IdentityModel.Client;
using WebApp.Models;
using WebApp.Services.Abstractions;

namespace WebApp.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IHttpClientFactory _clientFactory;

    public WeatherForecastService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecast()
    {
        var httpClient = _clientFactory.CreateClient();

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

        httpClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await httpClient.GetAsync("http://localhost:5002/weather");

        if (response.IsSuccessStatusCode)
        {
            var weatherForecasts = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
            return weatherForecasts;
        }
        else
        {
            throw new Exception("API request error");
        }
    }
}