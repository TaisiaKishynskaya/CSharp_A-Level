using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using WebApp.Models;
using WebApp.Services.Abstractions;

namespace WebApp.Services;

public class LoginService : ILoginService
{
    public async Task<DiscoveryDocumentResponse> GetDiscoveryDocumentAsync(HttpClient client)
    {
        return await client.GetDiscoveryDocumentAsync("https://localhost:5001");
    }

    public async Task<TokenResponse> RequestPasswordTokenAsync(HttpClient client, DiscoveryDocumentResponse discoveryDocument, LoginModel model)
    {
        return await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = discoveryDocument.TokenEndpoint,
            ClientId = "mvc_client",
            ClientSecret = "mvc_secret",
            Scope = "openid profile CatalogAPI WebBffAPI",
            UserName = model.Login,
            Password = model.Password
        });
    }

    public async Task<UserInfoResponse> GetUserInfoAsync(HttpClient client, DiscoveryDocumentResponse discoveryDocument, TokenResponse tokenResponse)
    {
        return await client.GetUserInfoAsync(new UserInfoRequest
        {
            Address = discoveryDocument.UserInfoEndpoint,
            Token = tokenResponse.AccessToken
        });
    }

    public async Task SignInAsync(HttpContext httpContext, UserInfoResponse userInfoResponse, TokenResponse tokenResponse)
    {
        var claims = new List<Claim>
    {
        new Claim("access_token", tokenResponse.AccessToken),
        new Claim("id_token", tokenResponse.IdentityToken ?? string.Empty),
        new Claim(ClaimTypes.Name, userInfoResponse.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value),
    };

        claims.AddRange(userInfoResponse.Claims);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties();

        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    }
}
