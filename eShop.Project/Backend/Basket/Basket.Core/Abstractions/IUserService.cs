using System.Security.Claims;

namespace Basket.Core.Abstractions;

public interface IUserService
{
    string GetUserId(ClaimsPrincipal user);
}