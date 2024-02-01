using System.Threading.Tasks;
using Basket.Domain.Models;

namespace Basket.Core.Abstractions;

public interface IBasketService
{
    Task<Domain.Models.Basket> GetBasket(string userId);
    Task<BasketItem> AddItem(string userId, ItemRequest itemRequest);
    Task<BasketItem> RemoveItem(string userId, int id);

}