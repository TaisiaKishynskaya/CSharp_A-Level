namespace WebApp.Services;

public class LoginService : ILoginService
{
    private readonly ApiClientSettings _bffClientSettings;
    private readonly ApiClientHelper _apiClientHelper;
    private readonly ILogger<LoginService> _logger;

    public LoginService(
        IOptions<MvcApiClientSettings> bffClientSettings,
        ApiClientHelper apiClientHelper,
        ILogger<LoginService> logger)
    {
        _bffClientSettings = bffClientSettings.Value;
        _apiClientHelper = apiClientHelper;
        _logger = logger;
    }

    public async Task<DiscoveryDocumentResponse> GetDiscoveryDocumentAsync(HttpClient client)
    {
        try
        {
            _logger.LogInformation("GetDiscoveryDocumentAsync started.");
            var result = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            _logger.LogInformation("GetDiscoveryDocumentAsync completed.");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetDiscoveryDocumentAsync.");
            throw;
        }
    }

    public async Task<TokenResponse> RequestPasswordTokenAsync(HttpClient client, DiscoveryDocumentResponse discoveryDocument, LoginModel model)
    {
        try
        {
            _logger.LogInformation($"RequestPasswordTokenAsync started. Login: {model.Login}");
            var result = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "mvc_client",
                ClientSecret = "mvc_secret",
                Scope = "openid profile CatalogAPI WebBffAPI",
                UserName = model.Login,
                Password = model.Password
            });
            _logger.LogInformation($"RequestPasswordTokenAsync completed. Login: {model.Login}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in RequestPasswordTokenAsync. Login: {model.Login}");
            throw;
        }
    }

    public async Task<UserInfoResponse> GetUserInfoAsync(HttpClient client, DiscoveryDocumentResponse discoveryDocument, TokenResponse tokenResponse)
    {
        try
        {
            _logger.LogInformation("GetUserInfoAsync started.");
            var result = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = discoveryDocument.UserInfoEndpoint,
                Token = tokenResponse.AccessToken
            });
            _logger.LogInformation("GetUserInfoAsync completed.");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetUserInfoAsync.");
            throw;
        }
    }

    public async Task SignInAsync(HttpContext httpContext, UserInfoResponse userInfoResponse, TokenResponse tokenResponse)
    {
        try
        {
            _logger.LogInformation("SignInAsync started.");
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
            _logger.LogInformation("SignInAsync completed.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SignInAsync.");
            throw;
        }
    }
}
