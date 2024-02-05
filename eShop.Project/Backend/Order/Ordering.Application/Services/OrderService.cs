using AutoMapper;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ordering.Core.Abstractions.Repositories;
using Ordering.Core.Abstractions.Services;
using Ordering.DataAccess.Entities;
using Ordering.Domain.Models;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Ordering.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository<OrderEntity> _orderRepository;
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IUserService _userService;
    private readonly ITransactionService _transactionService;

    public OrderService(
        IOrderRepository<OrderEntity> orderRepository,
        IMapper mapper,
        IHttpClientFactory httpClientFactory,
        IUserService userService,
        ITransactionService transactionService)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _httpClientFactory = httpClientFactory;
        _userService = userService;
        _transactionService = transactionService;
    }

    public async Task<IEnumerable<Order>> Get(int page, int size)
    {
        var ordersEntities = await _orderRepository.Get(page, size);

        var orders = _mapper.Map<IEnumerable<Order>>(ordersEntities);

        return orders;
    }

    public async Task<IEnumerable<Order>> GetByUser(string userId, int page, int size)
    {
        var ordersEntities = await _orderRepository.GetByUser(userId, page, size);

        var orders = _mapper.Map<IEnumerable<Order>>(ordersEntities);

        return orders;
    }

    public async Task<Order> GeyById(int id)
    {
        var orderEntity = await _orderRepository.GetById(id);

        var order = _mapper.Map<Order>(orderEntity);

        return order;
    }

    public async Task<Order> Add(Order order, string userId)
    {
        Order createdOrder = null;
        await _transactionService.ExecuteInTransactionAsync(async () =>
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var client = _httpClientFactory.CreateClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "basket_api_client",
                ClientSecret = "basket_api_client_secret",
                Scope = "BasketAPI",
            });

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var getBasketResponse = await apiClient.GetAsync($"http://localhost:5004/api/v1/basket/{userId}");

            var getBasketcontent = await getBasketResponse.Content.ReadAsStringAsync();
            var basket = JsonConvert.DeserializeObject<Basket>(getBasketcontent);

            if (basket.Items.Count == 0 || basket.Items == null)
            {
                throw new Exception("Basket is empty");
            }

            var orderEntity = _mapper.Map<OrderEntity>(order);
            orderEntity.UserId = userId;
            orderEntity.OrderDate = DateTime.UtcNow;
            orderEntity.Items = basket.Items.Select(item => new OrderItemEntity
            {
                ItemId = item.ItemId,
                Title = item.ItemTitle,
                Price = item.ItemPrice,
                Quantity = item.Quantity,
                PictureUrl = item.PictureUrl,
            }).ToList();

            orderEntity = await _orderRepository.Add(orderEntity);

            var deleteBasketResponse = await apiClient.DeleteAsync($"http://localhost:5004/api/v1/basket/{orderEntity.UserId}");

            createdOrder = _mapper.Map<Order>(orderEntity);
        });

        return createdOrder;
    }


    public async Task<Order> Update(Order order, ClaimsPrincipal userClaims)
    {
        var currentOrder = await _orderRepository.GetById(order.Id);
        if (currentOrder == null)
        {
            throw new KeyNotFoundException("Order not found.");
        }

        //if (!userClaims.Identity.Name.Equals(order.User.UserId))
        //{
        //    throw new UnauthorizedAccessException("You do not have permission to update this order.");
        //}

        currentOrder.Address = order.Address;

        foreach (var item in order.Items)
        {
            var catalogItem = await GetCatalogResponseById(item.ItemId);
            if (catalogItem == null)
            {
                throw new KeyNotFoundException($"Item with id {item.ItemId} not found in catalog.");
            }

            var currentOrderItem = currentOrder.Items.FirstOrDefault(i => i.Id == item.Id);
            if (currentOrderItem != null)
            {
                currentOrderItem.Quantity = item.Quantity;
            }
            else
            {
                var orderItemEntity = _mapper.Map<OrderItemEntity>(item);
                orderItemEntity.Title = catalogItem.Title;
                orderItemEntity.PictureUrl = catalogItem.PictureFile;
                orderItemEntity.Price = catalogItem.Price;
                currentOrder.Items.Add(orderItemEntity);
            }
        }

        currentOrder.Items.RemoveAll(item => !order.Items.Any(i => i.Id == item.Id));

        await _orderRepository.Update(currentOrder);

        return _mapper.Map<Order>(currentOrder);
    }



    private async Task<CatalogItem> GetCatalogResponseById(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "catalog_api_client",
            ClientSecret = "catalog_api_client_secret",
            Scope = "CatalogAPI"
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.GetAsync($"http://localhost:5000/api/v1/catalog/items/{id}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CatalogItem>(content);
        return result;
    }

    public async Task<Order> Delete(int id)
    {
        var orderEntity = await _orderRepository.Delete(id);
        return _mapper.Map<Order>(orderEntity);
    }
}


