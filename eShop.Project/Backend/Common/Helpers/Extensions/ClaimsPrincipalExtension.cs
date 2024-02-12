using IdentityModel;
using System.Security.Claims;

namespace Helpers.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string GetUserId(this ClaimsPrincipal userClaims)
    {
        Claim identifierClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier);
        return identifierClaim?.Value;
    }

    public static string GetUserName(this ClaimsPrincipal userClaims)
    {
        return userClaims.FindFirst(JwtClaimTypes.Name)?.Value;
    }

    public static string GetUserEmail(this ClaimsPrincipal userClaims)
    {
        return userClaims.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
    }
}
