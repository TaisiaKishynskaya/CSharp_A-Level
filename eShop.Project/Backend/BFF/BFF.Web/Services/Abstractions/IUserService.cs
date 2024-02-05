using System.Security.Claims;

namespace BFF.Web.Services.Abstractions
{
    public interface IUserService
    {
        string GetUserId(ClaimsPrincipal user);
    }
}