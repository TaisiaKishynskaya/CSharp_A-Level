namespace BFF.Tests;

public class BasketBffServiceTests
{
    private readonly Mock<ILogger<BasketBffService>> _mockLogger;
    private readonly Mock<IOptions<BasketApiClientSettings>> _mockOptions;
    private readonly Mock<IApiClientHelper> _mockApiClientHelper;
    private readonly BasketBffService _service;
    private readonly BasketApiClientSettings _basketSettings;

    public BasketBffServiceTests()
    {
        _mockLogger = new Mock<ILogger<BasketBffService>>();
        _mockOptions = new Mock<IOptions<BasketApiClientSettings>>();
        _mockApiClientHelper = new Mock<IApiClientHelper>();

        _basketSettings = new BasketApiClientSettings
        {
            DiscoveryUrl = "https://localhost:5001",
            ApiUrl = "http://localhost:5004/api/v1/basket",
            ClientId = "basket_api_client",
            ClientSecret = "basket_api_client_secret",
            Scope = "BasketAPI"
        };

        _mockOptions.Setup(o => o.Value).Returns(_basketSettings);

        _service = new BasketBffService(_mockLogger.Object, _mockOptions.Object, _mockApiClientHelper.Object);
    }

    [Fact]
    public async Task GetBasket_ReturnsBasket_WhenBasketExists()
    {
        // Arrange
        var fixture = new Fixture();
        var expectedBasket = fixture.Create<BasketResponse>();

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

        // Act
        var actualBasket = await _service.GetBasket("test-user");

        // Assert
        Assert.Equal(expectedBasket.UserId, actualBasket.UserId);
        Assert.Equal(expectedBasket.TotalPrice, actualBasket.TotalPrice);
        Assert.Equal(expectedBasket.TotalCount, actualBasket.TotalCount);
        // Проверьте остальные поля по необходимости
    }

    [Fact]
    public async Task GetBasket_ThrowsNotFoundException_WhenBasketDoesNotExist()
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

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetBasket("test-user"));
    }

    [Fact]
    public async Task AddBasketItem_ReturnsItemId_WhenItemIsAdded()
    {
        // Arrange
        var fixture = new Fixture();
        var itemRequest = fixture.Create<ItemRequest>();
        var expectedItemId = fixture.Create<int>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedItemId), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var actualItemId = await _service.AddBasketItem(itemRequest);

        // Assert
        Assert.Equal(expectedItemId, actualItemId);
    }

    [Fact]
    public async Task AddBasketItem_ReturnsMinusOne_WhenItemIsNotAdded()
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

        // Act
        var result = await _service.AddBasketItem(itemRequest);

        // Assert
        Assert.Equal(-1, result);
    }

    [Fact]
    public async Task DeleteBasketItem_ReturnsResponse_WhenItemIsDeleted()
    {
        // Arrange
        var fixture = new Fixture();
        var userId = fixture.Create<string>();
        var itemId = fixture.Create<int>();
        var expectedResponse = fixture.Create<DeleteBasketResponse>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedResponse), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var actualResponse = await _service.DeleteBasketItem(userId, itemId);

        // Assert
        Assert.Equal(expectedResponse.Id, actualResponse.Id);
        Assert.Equal(expectedResponse.Type, actualResponse.Type);
    }

    [Fact]
    public async Task DeleteBasketItem_ThrowsNotFoundException_WhenItemDoesNotExist()
    {
        // Arrange
        var fixture = new Fixture();
        var userId = fixture.Create<string>();
        var itemId = fixture.Create<int>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteBasketItem(userId, itemId));
    }
}
