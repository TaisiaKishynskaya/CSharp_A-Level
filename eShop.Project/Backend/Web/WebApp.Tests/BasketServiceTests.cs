namespace WebApp.Tests;

public class BasketServiceTests
{
    private readonly Mock<ILogger<BasketService>> _mockLogger;
    private readonly Mock<IOptions<MvcApiClientSettings>> _mockOptions;
    private readonly Mock<IApiClientHelper> _mockApiClientHelper;
    private readonly BasketService _service;
    private readonly MvcApiClientSettings _bffClientSettings;

    public BasketServiceTests()
    {
        _mockLogger = new Mock<ILogger<BasketService>>();
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

        _service = new BasketService(_mockOptions.Object, _mockApiClientHelper.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetBasket_ReturnsBasket_WhenBasketExists()
    {
        // Arrange
        var fixture = new Fixture();
        var expectedBasket = fixture.Create<BasketModel>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedBasket), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        var claims = new List<Claim> { new Claim("sub", "123") };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = principal };

        // Act
        var actualBasket = await _service.GetBasket(httpContext);

        // Assert
        Assert.Equal(expectedBasket.UserId, actualBasket.UserId);
    }

    [Fact]
    public async Task GetBasket_ThrowsException_WhenBasketDoesNotExist()
    {
        // Arrange
        var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        var claims = new List<Claim> { new Claim("sub", "123") };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = principal };

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.GetBasket(httpContext));
    }

    [Fact]
    public async Task AddBasketItem_ReturnsExpectedResult_WhenRequestIsSuccessful()
    {
        // Arrange
        var fixture = new Fixture();
        var itemRequest = fixture.Create<ItemRequest>();
        var expectedResult = fixture.Create<int>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedResult), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var actualResult = await _service.AddBasketItem(itemRequest);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public async Task AddBasketItem_ThrowsException_WhenRequestIsUnsuccessful()
    {
        // Arrange
        var fixture = new Fixture();
        var itemRequest = fixture.Create<ItemRequest>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.AddBasketItem(itemRequest));
    }

    [Fact]
    public async Task DeleteBasketItem_ReturnsExpectedResult_WhenRequestIsSuccessful()
    {
        // Arrange
        var fixture = new Fixture();
        var expectedResult = fixture.Create<DeleteBasketResponse>();
        var userId = "123";
        var itemId = 1;

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedResult), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var actualResult = await _service.DeleteBasketItem(userId, itemId);

        // Assert
        Assert.Equal(expectedResult.Id, actualResult.Id);
        Assert.Equal(expectedResult.Type, actualResult.Type);
    }

    [Fact]
    public async Task DeleteBasketItem_ThrowsException_WhenRequestIsUnsuccessful()
    {
        // Arrange
        var userId = "123";
        var itemId = 1;

        var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.DeleteBasketItem(userId, itemId));
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
