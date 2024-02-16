using Settings;

namespace Helpers.Abstractions;

public interface IApiClientHelper
{
    Task<HttpClient> CreateClientWithToken(ApiClientSettings settings);
}