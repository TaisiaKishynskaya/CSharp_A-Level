using Duende.IdentityServer.Models;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("catalog.fullaccess", "Full access to Catalog API")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "catalog",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                }
            },
            new Client
            {
                ClientId = "catalogswaggerui",
                ClientName = "Catalog Swagger UI",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,

                 RedirectUris = { "http://localhost:5000/swagger/oauth2-redirect.html" },
                 PostLogoutRedirectUris = { "http://localhost:5000/swagger/" },

                AllowedScopes =
                {
                    "catalog.fullaccess"
                }
            }
        };
}