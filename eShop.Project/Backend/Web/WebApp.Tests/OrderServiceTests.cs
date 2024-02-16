namespace WebApp.Tests;

public class OrderServiceTests
{
    private readonly Mock<ILogger<OrderService>> _mockLogger;
    private readonly Mock<IOptions<MvcApiClientSettings>> _mockOptions;
    private readonly Mock<IApiClientHelper> _mockApiClientHelper;
    private readonly OrderService _service;
    private readonly MvcApiClientSettings _bffClientSettings;

    public OrderServiceTests()
    {
        _mockLogger = new Mock<ILogger<OrderService>>();
        _mockOptions = new Mock<IOptions<MvcApiClientSettings>>();
        _mockApiClientHelper = new Mock<IApiClientHelper>();

        _bffClientSettings = new MvcApiClientSettings
        {
            DiscoveryUrl = "https://localhost:5001",
            ApiUrl = "http://localhost:5002/bff",
            ClientId = "mvc_client",
            ClientSecret = "mvc_secret",
            Scope = "WebBffAPI"
        };

        _mockOptions.Setup(o => o.Value).Returns(_bffClientSettings);

        _service = new OrderService(_mockOptions.Object, _mockApiClientHelper.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetOrdersByUser_ReturnsOrders_WhenUserHasOrders()
    {
        // Arrange
        var fixture = new Fixture();
        var expectedOrders = fixture.Create<IEnumerable<OrderModel>>();
        var userId = "123";

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

        var claims = new List<Claim> { new Claim("sub", userId) };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = principal };

        // Act
        var actualOrders = await _service.GetOrdersByUser(httpContext);

        // Assert
        Assert.Equal(expectedOrders.Count(), actualOrders.Count());
    }

    [Fact]
    public async Task GetOrdersByUser_ThrowsException_WhenRequestIsUnsuccessful()
    {
        // Arrange
        var userId = "123";

        var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        var claims = new List<Claim> { new Claim("sub", userId) };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = principal };

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.GetOrdersByUser(httpContext));
    }

    [Fact]
    public async Task AddOrder_ReturnsOrder_WhenRequestIsSuccessful()
    {
        // Arrange
        var fixture = new Fixture();
        var orderRequest = fixture.Create<OrderRequest>();
        var expectedOrder = fixture.Create<OrderModel>();

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
    public async Task AddOrder_ThrowsException_WhenRequestIsUnsuccessful()
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

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.AddOrder(orderRequest));
    }


    [Fact]
    public void FindUserId_ReturnsUserId_WhenUserIsAuthenticated()
    {
        // Arrange
        var userId = "123";
        var claims = new List<Claim> { new Claim("sub", userId) };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = principal };

        // Act
        var actualUserId = _service.FindUserId(httpContext);

        // Assert
        Assert.Equal(userId, actualUserId);
    }

    [Fact]
    public void FindUserId_ReturnsEmptyString_WhenUserIsNotAuthenticated()
    {
        // Arrange
        var claims = new List<Claim> { new Claim("sub", "") };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = principal };

        // Act
        var actualUserId = _service.FindUserId(httpContext);

        // Assert
        Assert.Equal("", actualUserId);
    }
}
