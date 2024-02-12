namespace Ordering.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository<OrderEntity> _orderRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly ITransactionService _transactionService;
    private readonly ApiClientSettings _catalogSettings;
    private readonly ApiClientSettings _basketSettings;
    private readonly ApiClientHelper _apiClientHelper;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        IOrderRepository<OrderEntity> orderRepository,
        IMapper mapper,
        IUserService userService,
        ITransactionService transactionService,
        IOptions<CatalogApiClientSettings> catalogSettings,
        IOptions<BasketApiClientSettings> basketSettings,
        ApiClientHelper apiClientHelper,
        ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _userService = userService;
        _transactionService = transactionService;
        _catalogSettings = catalogSettings.Value;
        _basketSettings = basketSettings.Value;
        _apiClientHelper = apiClientHelper;
        _logger = logger;
    }

    public async Task<IEnumerable<Order>> Get(int page, int size)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var ordersEntities = await _orderRepository.Get(page, size);
            var endTime = DateTime.UtcNow;

            _logger.LogInformation($"Operation: Get, Start Time: {startTime}, End Time: {endTime}, Status: Success");
            var orders = _mapper.Map<IEnumerable<Order>>(ordersEntities);
            return orders;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<Order> GeyById(int id)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var orderEntity = await _orderRepository.GetById(id);
            var endTime = DateTime.UtcNow;

            if (orderEntity == null)
            {
                _logger.LogError($"Error: Order with id = {id} not found, Stack Trace: {Environment.StackTrace}");
                throw new NotFoundException($"Order with id = {id} not found");
            }

            _logger.LogInformation($"Operation: GeyById, Id: {id}, Start Time: {startTime}, End Time: {endTime}, Status: Success");
            var order = _mapper.Map<Order>(orderEntity);
            return order;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetByUser(string userId, int page, int size)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var ordersEntities = await _orderRepository.GetByUser(userId, page, size);
            var endTime = DateTime.UtcNow;

            _logger.LogInformation($"Operation: GetByUser, UserId: {userId}, Start Time: {startTime}, End Time: {endTime}, Status: Success");
            var orders = _mapper.Map<IEnumerable<Order>>(ordersEntities);
            return orders;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<Order> Add(Order order, string userId)
    {
        Order createdOrder = null;
        await _transactionService.ExecuteInTransactionAsync(async () =>
        {
            try
            {
                var startTime = DateTime.UtcNow;
                var user = await _userService.GetUserById(userId);
                if (user == null)
                {
                    _logger.LogError($"Error: User not found, Stack Trace: {Environment.StackTrace}");
                    throw new Exception("User not found");
                }

                var basket = await GetBasketByUserId(userId);

                if (basket.Items.Count == 0 || basket.Items == null)
                {
                    _logger.LogError($"Error: Basket is empty, Stack Trace: {Environment.StackTrace}");
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

                var apiClient = await _apiClientHelper.CreateClientWithToken(_basketSettings);
                var deleteBasketResponse = await apiClient.DeleteAsync($"{_basketSettings.ApiUrl}/{orderEntity.UserId}");

                createdOrder = _mapper.Map<Order>(orderEntity);
                var endTime = DateTime.UtcNow;

                _logger.LogInformation($"Operation: Add, UserId: {userId}, Start Time: {startTime}, End Time: {endTime}, Status: Success");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
                throw;
            }
        });

        return createdOrder;
    }

    public async Task<Order> Update(Order order, ClaimsPrincipal userClaims)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var currentOrder = await _orderRepository.GetById(order.Id);
            if (currentOrder == null)
            {
                _logger.LogError($"Error: Order not found, Stack Trace: {Environment.StackTrace}");
                throw new KeyNotFoundException("Order not found.");
            }

            currentOrder.Address = order.Address;

            foreach (var item in order.Items)
            {
                var catalogItem = await GetCatalogItemById(item.ItemId);
                if (catalogItem == null)
                {
                    _logger.LogError($"Error: Item with id {item.ItemId} not found in catalog, Stack Trace: {Environment.StackTrace}");
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
            var endTime = DateTime.UtcNow;

            _logger.LogInformation($"Operation: Update, OrderId: {order.Id}, Start Time: {startTime}, End Time: {endTime}, Status: Success");
            return _mapper.Map<Order>(currentOrder);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<Order> Delete(int id)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var orderEntity = await _orderRepository.Delete(id);
            var endTime = DateTime.UtcNow;

            _logger.LogInformation($"Operation: Delete, OrderId: {id}, Start Time: {startTime}, End Time: {endTime}, Status: {(orderEntity != null ? "Success" : "Failure")}");
            return _mapper.Map<Order>(orderEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    private async Task<CatalogItem> GetCatalogItemById(int id)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var apiClient = await _apiClientHelper.CreateClientWithToken(_catalogSettings);
            var response = await apiClient.GetAsync($"{_catalogSettings.ApiUrl}/items/{id}");

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CatalogItem>(content);
            var endTime = DateTime.UtcNow;

            _logger.LogInformation($"Operation: GetCatalogItemById, ItemId: {id}, Start Time: {startTime}, End Time: {endTime}, Status: {(result != null ? "Success" : "Failure")}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    private async Task<Basket> GetBasketByUserId(string userId)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var apiClient = await _apiClientHelper.CreateClientWithToken(_basketSettings);
            var response = await apiClient.GetAsync($"{_basketSettings.ApiUrl}/{userId}");

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Basket>(content);
            var endTime = DateTime.UtcNow;

            _logger.LogInformation($"Operation: GetBasketByUserId, UserId: {userId}, Start Time: {startTime}, End Time: {endTime}, Status: {(result != null ? "Success" : "Failure")}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }
}


