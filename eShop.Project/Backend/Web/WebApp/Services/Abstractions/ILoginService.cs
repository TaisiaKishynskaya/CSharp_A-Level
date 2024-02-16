namespace WebApp.Services.Abstractions;

public interface ILoginService
{
    Task<DiscoveryDocumentResponse> GetDiscoveryDocumentAsync(HttpClient client);
    Task<TokenResponse> RequestPasswordTokenAsync(HttpClient client, DiscoveryDocumentResponse discoveryDocument, LoginModel model);
    Task<UserInfoResponse> GetUserInfoAsync(HttpClient client, DiscoveryDocumentResponse discoveryDocument, TokenResponse tokenResponse);
    Task SignInAsync(HttpContext httpContext, UserInfoResponse userInfoResponse, TokenResponse tokenResponse);
}