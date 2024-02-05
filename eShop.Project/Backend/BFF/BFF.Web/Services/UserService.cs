using System.Security.Claims;
using BFF.Web.Services.Abstractions;

namespace BFF.Web.Services;

public class UserService : IUserService
{
    public string GetUserId(ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
