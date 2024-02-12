using IdentityModel.Client;
using Settings;

namespace Helpers;

public class ApiClientHelper
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiClientHelper(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<HttpClient> CreateClientWithToken(ApiClientSettings settings)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync(settings.DiscoveryUrl);

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = settings.ClientId,
            ClientSecret = settings.ClientSecret,
            Scope = settings.Scope
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        return apiClient;
    }
}
