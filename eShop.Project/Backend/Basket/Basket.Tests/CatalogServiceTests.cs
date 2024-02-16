namespace Basket.Tests;

public class CatalogServiceTests
{
    private readonly Mock<ILogger<CatalogService>> _mockLogger;
    private readonly Mock<IApiClientHelper> _mockApiClientHelper;
    private readonly Mock<IOptions<ApiClientSettings>> _mockOptions;
    private readonly CatalogService _service;

    public CatalogServiceTests()
    {
        _mockLogger = new Mock<ILogger<CatalogService>>();
        _mockApiClientHelper = new Mock<IApiClientHelper>();
        _mockOptions = new Mock<IOptions<ApiClientSettings>>();
        _mockOptions.Setup(o => o.Value).Returns(new ApiClientSettings
        {
            DiscoveryUrl = "https://localhost:5001",
            ApiUrl = "http://localhost:5000/api/v1/catalog",
            ClientId = "catalog_api_client",
            ClientSecret = "catalog_api_client_secret",
            Scope = "CatalogAPI"
        });

        _service = new CatalogService(_mockOptions.Object, _mockApiClientHelper.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetItemById_ReturnsItem_WhenItemExists()
    {
        // Arrange
        var expectedItem = new CatalogItem { Id = 1 };
        var responseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(expectedItem), Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>())).ReturnsAsync(httpClient);

        // Act
        var result = await _service.GetItemById(1);

        // Assert
        Assert.Equal(expectedItem.Id, result.Id);
    }

    [Fact]
    public async Task GetItemById_ThrowsArgumentException_WhenItemDoesNotExist()
    {
        // Arrange
        var responseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound,
            Content = new StringContent("", Encoding.UTF8, "application/json")
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockApiClientHelper.Setup(a => a.CreateClientWithToken(It.IsAny<ApiClientSettings>())).ReturnsAsync(httpClient);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.GetItemById(1));
    }
}