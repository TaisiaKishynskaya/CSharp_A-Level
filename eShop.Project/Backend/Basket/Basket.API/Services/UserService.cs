using Basket.Core.Abstractions;
using System.Security.Claims;

namespace Basket.API.Services;

public class UserService : IUserService
{
    public string GetUserId(ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
