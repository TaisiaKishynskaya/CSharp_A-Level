using Ordering.Domain.Models;
using System.Security.Claims;

namespace Ordering.Application.Services;

public interface IUserService
{
    Task<IEnumerable<User>> Get(int page, int size);
    Task<User> Add(ClaimsPrincipal userClaims);
}