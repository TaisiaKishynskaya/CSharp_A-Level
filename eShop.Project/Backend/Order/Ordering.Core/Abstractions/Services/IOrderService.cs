using Ordering.Domain.Models;
using System.Security.Claims;

namespace Ordering.Core.Abstractions.Services;

public interface IOrderService
{
    //Task<Order> Add(Order order, ClaimsPrincipal userClaims);
    Task<Order> Add(Order order, string userId);
    Task<IEnumerable<Order>> Get(int page, int size);
    Task<IEnumerable<Order>> GetByUser(string userId, int page, int size);
    Task<Order> GeyById(int id);
    Task<Order> Update(Order order, ClaimsPrincipal userClaims);
    Task<Order> Delete(int id);
}