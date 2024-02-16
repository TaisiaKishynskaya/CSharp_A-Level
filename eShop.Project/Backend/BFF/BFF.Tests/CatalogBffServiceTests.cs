namespace BFF.Tests;

public class CatalogBffServiceTests
{
    private readonly Mock<ILogger<CatalogBffService>> _mockLogger;
    private readonly Mock<IOptions<CatalogApiClientSettings>> _mockOptions;
    private readonly Mock<IApiClientHelper> _mockApiClientHelper;
    private readonly CatalogBffService _service;
    private readonly CatalogApiClientSettings _catalogSettings;

    public CatalogBffServiceTests()
    {
        _mockLogger = new Mock<ILogger<CatalogBffService>>();
        _mockOptions = new Mock<IOptions<CatalogApiClientSettings>>();
        _mockApiClientHelper = new Mock<IApiClientHelper>();

        _catalogSettings = new CatalogApiClientSettings
        {
            DiscoveryUrl = "https://localhost:5001",
            ApiUrl = "http://localhost:5000/api/v1/catalog",
            ClientId = "catalog_api_client",
            ClientSecret = "catalog_api_client_secret",
            Scope = "CatalogAPI"
        };

        _mockOptions.Setup(o => o.Value).Returns(_catalogSettings);

        _service = new CatalogBffService(_mockLogger.Object, _mockOptions.Object, _mockApiClientHelper.Object);
    }

    [Fact]
    public async Task GetBrands_ReturnsBrands_WhenBrandsExist()
    {
        // Arrange
        var expectedBrands = new PaginatedResponse<CatalogBrandResponse>
        {
            Total = 1,
            Data = new List<CatalogBrandResponse>
        {
            new CatalogBrandResponse
            {
                Id = 1,
                Title = "Test Brand"
            }
        }
        };

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
        var actualBrands = await _service.GetBrands(1, 10);

        // Assert
        Assert.Equal(expectedBrands.Total, actualBrands.Total);
        Assert.Equal(expectedBrands.Data.Count(), actualBrands.Data.Count());

        var expectedDataList = expectedBrands.Data.ToList();
        var actualDataList = actualBrands.Data.ToList();

        for (int i = 0; i < expectedDataList.Count; i++)
        {
            Assert.Equal(expectedDataList[i].Id, actualDataList[i].Id);
            Assert.Equal(expectedDataList[i].Title, actualDataList[i].Title);
        }
    }

    [Fact]
    public async Task GetBrands_ReturnsNull_WhenBrandsDoNotExist()
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
        var result = await _service.GetBrands(1, 10);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetTypes_ReturnsTypes_WhenTypesExist()
    {
        // Arrange
        var expectedTypes = new PaginatedResponse<CatalogTypeResponse>
        {
            Total = 1,
            Data = new List<CatalogTypeResponse>
        {
            new CatalogTypeResponse
            {
                Id = 1,
                Title = "Test Type"
            }
        }
        };

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
        var actualTypes = await _service.GetTypes(1, 10);

        // Assert
        Assert.Equal(expectedTypes.Total, actualTypes.Total);
        Assert.Equal(expectedTypes.Data.Count(), actualTypes.Data.Count());

        var expectedDataList = expectedTypes.Data.ToList();
        var actualDataList = actualTypes.Data.ToList();

        for (int i = 0; i < expectedDataList.Count; i++)
        {
            Assert.Equal(expectedDataList[i].Id, actualDataList[i].Id);
            Assert.Equal(expectedDataList[i].Title, actualDataList[i].Title);
        }
    }

    [Fact]
    public async Task GetTypes_ReturnsNull_WhenTypesDoNotExist()
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
        var result = await _service.GetTypes(1, 10);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetItems_ReturnsItems_WhenItemsExist()
    {
        // Arrange
        var fixture = new Fixture();
        var expectedItems = fixture.Create<PaginatedResponse<CatalogItemResponse>>();

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
        var actualItems = await _service.GetItems(1, 10);

        // Assert
        Assert.Equal(expectedItems.Total, actualItems.Total);
        Assert.Equal(expectedItems.Data.Count(), actualItems.Data.Count());

        var expectedDataList = expectedItems.Data.ToList();
        var actualDataList = actualItems.Data.ToList();

        for (int i = 0; i < expectedDataList.Count; i++)
        {
            Assert.Equal(expectedDataList[i].Id, actualDataList[i].Id);
            Assert.Equal(expectedDataList[i].Title, actualDataList[i].Title);
        }
    }

    [Fact]
    public async Task GetItems_ReturnsNull_WhenItemsDoNotExist()
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
        var result = await _service.GetItems(1, 10);

        // Assert
        Assert.Null(result);
    }
}
