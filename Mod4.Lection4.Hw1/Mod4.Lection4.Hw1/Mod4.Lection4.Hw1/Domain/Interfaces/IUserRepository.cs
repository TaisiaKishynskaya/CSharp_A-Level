using Mod4.Lection4.Hw1.Domain.Models;
using System.Security.Claims;

namespace Mod4.Lection4.Hw1.Domain.Interfaces;

public interface IUserRepository
{
    Task CreateUserAsync(User @user);
    Task UpdateUserAsync(User @user);
    Task DeleteUserAsync(User @user);
    Task<User> GetUserAsync(Guid @user);
    Task<IReadOnlyCollection<User>> GetAllUsersAsync();
    Task AddOrders(Guid id, ICollection<Order> orders);
}
