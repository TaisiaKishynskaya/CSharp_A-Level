using BFF.Web.Responses;

namespace BFF.Web.Services.Abstractions;

public interface IBasketService
{
    Task<Basket> GetBasket(string userId);
    Task<int> AddBasketItem(ItemRequest item);
    Task<int> DeleteBasketItem(string userId, int itemId);
}