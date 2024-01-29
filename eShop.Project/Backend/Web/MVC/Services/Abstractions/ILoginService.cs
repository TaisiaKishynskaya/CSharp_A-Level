using IdentityModel.Client;
using WebApp.Models;

namespace WebApp.Services.Abstractions;

public interface ILoginService
{
    Task<DiscoveryDocumentResponse> GetDiscoveryDocumentAsync(HttpClient client);
    Task<UserInfoResponse> GetUserInfoAsync(HttpClient client, DiscoveryDocumentResponse discoveryDocument, TokenResponse tokenResponse);
    Task<TokenResponse> RequestPasswordTokenAsync(HttpClient client, DiscoveryDocumentResponse discoveryDocument, LoginModel model);
    Task SignInAsync(HttpContext httpContext, UserInfoResponse userInfoResponse, TokenResponse tokenResponse);
}