using System;
using System.Linq;
using System.Threading.Tasks;
using Basket.Core.Abstractions;
using Basket.Domain.Models;
using Newtonsoft.Json;

namespace Basket.API.Services;

public class BasketService : IBasketService
{
    private readonly ICatalogService _catalogService;
    private readonly ICacheService _cacheService;
    private readonly IUserService _userService;

    public BasketService(
        ICatalogService catalogService,
        ICacheService cacheService,
        IUserService userService)
    {
        _catalogService = catalogService;
        _cacheService = cacheService;
        _userService = userService;
    }

    public async Task<Domain.Models.Basket> GetBasket(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
        }

        var data = await _cacheService.Get(userId);
        if (data == null)
        {
            return await CreateBasket(userId);
        }
        return JsonConvert.DeserializeObject<Domain.Models.Basket>(data);
    }

    public async Task<BasketItem> AddItem(string userId, ItemRequest itemRequest)
    {
        var item = await _catalogService.GetItemById(itemRequest.ItemId);
        if (item == null)
        {
            throw new ArgumentException($"Item with id = {itemRequest.ItemId} not found");
        }

        var basket = await GetBasket(userId);

        var basketItem = basket.Items.FirstOrDefault(i => i.ItemId == item.Id);
        if (basketItem != null)
        {
            basketItem.Quantity++;
        }
        else
        {
            basketItem = new BasketItem
            {
                ItemId = item.Id,
                ItemTitle = item.Title,
                ItemPrice = item.Price,
                PictureUrl = item.PictureFile,
                Quantity = 1
            };
            basket.Items.Add(basketItem);
        }

        await UpdateBasket(basket);
        return basketItem;
    }

    public async Task<BasketItem> RemoveItem(string userId, int id)
    {
        var basket = await GetBasket(userId);

        var basketItem = basket.Items.FirstOrDefault(i => i.ItemId == id);
        if (basketItem == null)
        {
            throw new ArgumentException($"Item with id = {id} not found in the basket");
        }

        if (basketItem.Quantity > 1) basketItem.Quantity--;
        else basket.Items.Remove(basketItem);

        await UpdateBasket(basket);

        return basketItem;
    }

    private async Task<Domain.Models.Basket> CreateBasket(string userId)
    {
        Domain.Models.Basket newBasket = new Domain.Models.Basket
        {
            UserId = userId,
        };

        await UpdateBasket(newBasket);
        return newBasket;
    }

    private async Task<Domain.Models.Basket> UpdateBasket(Domain.Models.Basket basket)
    {
        var createdBasket = await _cacheService.Set(
            basket.UserId,
            JsonConvert.SerializeObject(basket));

        return await GetBasket(basket.UserId);
    }

}



