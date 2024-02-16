namespace BFF.Tests;

public class OrderBffServiceTests
{
    private readonly Mock<ILogger<OrderBffService>> _mockLogger;
    private readonly Mock<IOptions<OrderApiClientSettings>> _mockOptions;
    private readonly Mock<IApiClientHelper> _mockApiClientHelper;
    private readonly OrderBffService _service;
    private readonly OrderApiClientSettings _orderSettings;

    public OrderBffServiceTests()
    {
        _mockLogger = new Mock<ILogger<OrderBffService>>();
        _mockOptions = new Mock<IOptions<OrderApiClientSettings>>();
        _mockApiClientHelper = new Mock<IApiClientHelper>();

        _orderSettings = new OrderApiClientSettings
        {
            DiscoveryUrl = "https://localhost:5001",
            ApiUrl = "http://localhost:5005/api/v1/orders",
            ClientId = "order_api_client",
            ClientSecret = "order_api_client_secret",
            Scope = "OrderAPI"
        };

        _mockOptions.Setup(o => o.Value).Returns(_orderSettings);

        _service = new OrderBffService(_mockLogger.Object, _mockOptions.Object, _mockApiClientHelper.Object);
    }

    [Fact]
    public async Task GetOrderById_ReturnsOrder_WhenOrderExists()
    {
        // Arrange
        var fixture = new Fixture();
        var orderId = fixture.Create<int>();
        var expectedOrder = fixture.Create<OrderResponse>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedOrder), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var actualOrder = await _service.GetOrderById(orderId);

        // Assert
        Assert.Equal(expectedOrder.Id, actualOrder.Id);
    }

    [Fact]
    public async Task GetOrderById_ThrowsNotFoundException_WhenOrderDoesNotExist()
    {
        // Arrange
        var fixture = new Fixture();
        var orderId = fixture.Create<int>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetOrderById(orderId));
    }

    [Fact]
    public async Task GetOrdersByUser_ReturnsOrders_WhenOrdersExist()
    {
        // Arrange
        var fixture = new Fixture();
        var userId = fixture.Create<string>();
        var expectedOrders = fixture.Create<IEnumerable<OrderResponse>>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedOrders), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var actualOrders = await _service.GetOrdersByUser(userId, 1, 10);

        // Assert
        Assert.Equal(expectedOrders.Count(), actualOrders.Count());
    }

    [Fact]
    public async Task GetOrdersByUser_ThrowsNotFoundException_WhenOrdersDoNotExist()
    {
        // Arrange
        var fixture = new Fixture();
        var userId = fixture.Create<string>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetOrdersByUser(userId, 1, 10));
    }

    [Fact]
    public async Task GetOrders_ReturnsOrders_WhenOrdersExist()
    {
        // Arrange
        var fixture = new Fixture();
        var expectedOrders = fixture.Create<IEnumerable<OrderResponse>>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedOrders), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var actualOrders = await _service.GetOrders(1, 10);

        // Assert
        Assert.Equal(expectedOrders.Count(), actualOrders.Count());
    }

    [Fact]
    public async Task GetOrders_ReturnsNull_WhenOrdersDoNotExist()
    {
        // Arrange
        var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var result = await _service.GetOrders(1, 10);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddOrder_ReturnsOrder_WhenOrderIsAdded()
    {
        // Arrange
        var fixture = new Fixture();
        var orderRequest = fixture.Create<OrderRequest>();
        var expectedOrder = fixture.Create<OrderResponse>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedOrder), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var actualOrder = await _service.AddOrder(orderRequest);

        // Assert
        Assert.Equal(expectedOrder.Id, actualOrder.Id);
    }

    [Fact]
    public async Task AddOrder_ReturnsNull_WhenOrderIsNotAdded()
    {
        // Arrange
        var fixture = new Fixture();
        var orderRequest = fixture.Create<OrderRequest>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var result = await _service.AddOrder(orderRequest);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteOrder_ReturnsOrder_WhenOrderIsDeleted()
    {
        // Arrange
        var fixture = new Fixture();
        var orderId = fixture.Create<int>();
        var expectedOrder = fixture.Create<OrderResponse>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedOrder), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var actualOrder = await _service.DeleteOrder(orderId);

        // Assert
        Assert.Equal(expectedOrder.Id, actualOrder.Id);
    }

    [Fact]
    public async Task DeleteOrder_ThrowsNotFoundException_WhenOrderDoesNotExist()
    {
        // Arrange
        var fixture = new Fixture();
        var orderId = fixture.Create<int>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteOrder(orderId));
    }
}