namespace WebApp.Tests;

public class CatalogServiceTests
{
    private readonly Mock<ILogger<CatalogService>> _mockLogger;
    private readonly Mock<IOptions<MvcApiClientSettings>> _mockOptions;
    private readonly Mock<IApiClientHelper> _mockApiClientHelper;
    private readonly CatalogService _service;
    private readonly MvcApiClientSettings _bffClientSettings;

    public CatalogServiceTests()
    {
        _mockLogger = new Mock<ILogger<CatalogService>>();
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

        _service = new CatalogService(_mockOptions.Object, _mockApiClientHelper.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetCatalogItems_ReturnsItems_WhenItemsExist()
    {
        // Arrange
        var fixture = new Fixture();
        var expectedItems = fixture.Create<PaginatedDataModel<CatalogItemModel>>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedItems), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var actualItems = await _service.GetCatalogItems(1, 10, "price_asc");

        // Assert
        Assert.Equal(expectedItems.Data.Count(), actualItems.Data.Count());
    }

    [Fact]
    public async Task GetCatalogItems_ThrowsException_WhenItemsDoNotExist()
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

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.GetCatalogItems(1, 10, "price_asc"));
    }

    [Fact]
    public async Task GetCatalogTypes_ReturnsTypes_WhenTypesExist()
    {
        // Arrange
        var fixture = new Fixture();
        var expectedTypes = fixture.Create<PaginatedDataModel<CatalogTypeModel>>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedTypes), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var actualTypes = await _service.GetCatalogTypes();

        // Assert
        Assert.Equal(expectedTypes.Data.Count(), actualTypes.Data.Count());
    }

    [Fact]
    public async Task GetCatalogTypes_ThrowsException_WhenTypesDoNotExist()
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

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.GetCatalogTypes());
        }

    [Fact]
    public async Task GetCatalogBrands_ReturnsBrands_WhenBrandsExist()
    {
        // Arrange
        var fixture = new Fixture();
        var expectedBrands = fixture.Create<PaginatedDataModel<CatalogBrandModel>>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedBrands), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var actualBrands = await _service.GetCatalogBrands();

        // Assert
        Assert.Equal(expectedBrands.Data.Count(), actualBrands.Data.Count());
    }

    [Fact]
    public async Task GetCatalogBrands_ThrowsException_WhenBrandsDoNotExist()
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

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.GetCatalogBrands());
    }

    [Fact]
    public async Task GetCatalogItemById_ReturnsItem_WhenItemExists()
    {
        // Arrange
        var fixture = new Fixture();
        var itemId = fixture.Create<int>();
        var expectedItem = fixture.Create<CatalogItemModel>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedItem), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act
        var actualItem = await _service.GetCatalogItemById(itemId);

        // Assert
        Assert.Equal(expectedItem.Id, actualItem.Id);
    }

    [Fact]
    public async Task GetCatalogItemById_ThrowsException_WhenItemDoesNotExist()
    {
        // Arrange
        var fixture = new Fixture();
        var itemId = fixture.Create<int>();

        var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>()))
                            .ReturnsAsync(httpClient);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.GetCatalogItemById(itemId));
    }


}