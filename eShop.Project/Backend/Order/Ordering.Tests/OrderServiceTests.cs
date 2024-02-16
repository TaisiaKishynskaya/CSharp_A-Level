using AutoFixture;
using Moq.Protected;
using Newtonsoft.Json;
using Ordering.Application.Infrastructure.Exceptions;
using Ordering.Domain.Models;
using System.Net;

namespace Ordering.Tests;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository<OrderEntity>> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<ITransactionService> _mockTransactionService;
    private readonly Mock<IOptions<CatalogApiClientSettings>> _mockCatalogSettings;
    private readonly Mock<IOptions<BasketApiClientSettings>> _mockBasketSettings;
    private readonly Mock<IApiClientHelper> _mockApiClientHelper;
    private readonly Mock<ILogger<OrderService>> _mockLogger;
    private readonly OrderService _orderService;

    private readonly Mock<IUserRepository<UserEntity>> _mockUserRepo;

    public OrderServiceTests()
    {
        _mockRepo = new Mock<IOrderRepository<OrderEntity>>();
        _mockMapper = new Mock<IMapper>();
        _mockUserService = new Mock<IUserService>();
        _mockTransactionService = new Mock<ITransactionService>();
        _mockCatalogSettings = new Mock<IOptions<CatalogApiClientSettings>>();
        _mockBasketSettings = new Mock<IOptions<BasketApiClientSettings>>();
        _mockApiClientHelper = new Mock<IApiClientHelper>();
        _mockLogger = new Mock<ILogger<OrderService>>();

        _mockUserRepo = new Mock<IUserRepository<UserEntity>>();

        _orderService = new OrderService(
            _mockRepo.Object,
            _mockMapper.Object,
            _mockUserService.Object,
            _mockTransactionService.Object,
            _mockCatalogSettings.Object,
            _mockBasketSettings.Object,
            _mockApiClientHelper.Object,
            _mockLogger.Object);

        _mockCatalogSettings.Setup(o => o.Value).Returns(new CatalogApiClientSettings
        {
            DiscoveryUrl = "https://localhost:5001",
            ApiUrl = "http://localhost:5000/api/v1/catalog",
            ClientId = "catalog_api_client",
            ClientSecret = "catalog_api_client_secret",
            Scope = "CatalogAPI"
        });

        _mockBasketSettings.Setup(o => o.Value).Returns(new BasketApiClientSettings
        {
            DiscoveryUrl = "https://localhost:5001",
            ApiUrl = "http://localhost:5004/api/v1/basket",
            ClientId = "basket_api_client",
            ClientSecret = "basket_api_client_secret",
            Scope = "BasketAPI"
        });
    }

    [Fact]
    public async Task Get_ReturnsOrders_WhenOrdersExist()
    {
        // Arrange
        var fixture = new Fixture();
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var page = 1;
        var size = 10;

        var expectedOrders = fixture.Build<OrderEntity>()
            .Without(o => o.Items) 
            .CreateMany(2)
            .ToList();

        _mockRepo.Setup(r => r.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedOrders);

        var mappedOrders = fixture.CreateMany<Order>(2).ToList();
        _mockMapper.Setup(m => m.Map<IEnumerable<Order>>(It.IsAny<IEnumerable<OrderEntity>>())).Returns(mappedOrders);

        // Act
        var result = await _orderService.Get(page, size);

        // Assert
        Assert.Equal(expectedOrders.Count, result.Count());
        Assert.All(result, order => Assert.Contains(order, mappedOrders));
    }

    [Fact]
    public async Task Get_ThrowsException_WhenExceptionOccurs()
    {
        // Arrange
        var page = 1;
        var size = 10;
        _mockRepo.Setup(r => r.Get(page, size)).ThrowsAsync(new Exception());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _orderService.Get(page, size));
    }

    [Fact]
    public async Task GeyById_ReturnsOrder_WhenOrderExists()
    {
        // Arrange
        var fixture = new Fixture();
        var expectedOrder = fixture.Build<OrderEntity>()
            .Without(o => o.Items)
            .Create();

        _mockRepo.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(expectedOrder);

        var mappedOrder = fixture.Create<Order>();
        _mockMapper.Setup(m => m.Map<Order>(It.IsAny<OrderEntity>())).Returns(mappedOrder);

        // Act
        var result = await _orderService.GeyById(expectedOrder.Id);

        // Assert
        Assert.Equal(mappedOrder, result);
    }

    [Fact]
    public async Task GeyById_ThrowsNotFoundException_WhenOrderDoesNotExist()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((OrderEntity)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _orderService.GeyById(1));
    }

    [Fact]
    public async Task GetByUser_ReturnsOrders_WhenOrdersExistForUser()
    {
        // Arrange
        var fixture = new Fixture();
        var userId = "testUser";
        var page = 1;
        var size = 10;

        var expectedOrders = fixture.Build<OrderEntity>()
          .Without(o => o.Items)
          .CreateMany(2)
          .ToList();

        _mockRepo.Setup(r => r.GetByUser(userId, It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(expectedOrders);

        var mappedOrders = fixture.CreateMany<Order>(2).ToList();
        _mockMapper.Setup(m => m.Map<IEnumerable<Order>>(It.IsAny<IEnumerable<OrderEntity>>())).Returns(mappedOrders);

        // Act
        var result = await _orderService.GetByUser(userId, page, size);

        // Assert
        Assert.Equal(expectedOrders.Count, result.Count());
        Assert.All(result, order => Assert.Contains(order, mappedOrders));
    }

    [Fact]
    public async Task GetByUser_ReturnsEmpty_WhenNoOrdersExistForUser()
    {
        // Arrange
        var userId = "testUser";
        var page = 1;
        var size = 10;

        _mockRepo.Setup(r => r.GetByUser(userId, It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<OrderEntity>());

        // Act
        var result = await _orderService.GetByUser(userId, page, size);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task Add_ShouldAddOrder_WhenUserExistsAndBasketIsNotEmpty()
    {
        // Arrange
        var fixture = new Fixture();
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var userId = fixture.Create<string>();
        var order = fixture.Build<Order>()
            .With(o => o.User, new User { UserId = userId })
            .With(o => o.Items, new List<OrderItem> { fixture.Create<OrderItem>() })
            .Create();

        var user = new User { UserId = userId };
        _mockUserService.Setup(s => s.GetUserById(userId)).ReturnsAsync(user);

        var basket = new Basket { Items = new List<BasketItem> { fixture.Create<BasketItem>() } };
        var basketJson = JsonConvert.SerializeObject(basket);
        var httpMessageHandler = new Mock<HttpMessageHandler>();
        httpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(basketJson) });

        var httpClient = new HttpClient(httpMessageHandler.Object);
        _mockApiClientHelper.Setup(s => s.CreateClientWithToken(_mockBasketSettings.Object.Value)).ReturnsAsync(httpClient);

        var orderEntity = fixture.Create<OrderEntity>();
        _mockMapper.Setup(m => m.Map<OrderEntity>(order)).Returns(orderEntity);

        _mockRepo.Setup(r => r.Add(It.Is<OrderEntity>(oe => oe.UserId == userId && oe.OrderDate != null))).ReturnsAsync(orderEntity);

        var orderResult = fixture.Create<Order>();
        _mockMapper.Setup(m => m.Map<Order>(orderEntity)).Returns(orderResult);

        // Act
        var result = await _orderService.Add(order, userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderResult, result);
        _mockRepo.Verify(r => r.Add(orderEntity), Times.Once);
    }

}
