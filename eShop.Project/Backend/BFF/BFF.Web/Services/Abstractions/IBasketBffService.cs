namespace BFF.Web.Services.Abstractions;

public interface IBasketBffService
{
    Task<BasketResponse> GetBasket(string userId);
    Task<int> AddBasketItem(ItemRequest item);
    Task<DeleteBasketResponse> DeleteBasketItem(string userId, int itemId);
}