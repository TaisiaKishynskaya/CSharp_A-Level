namespace Basket.Tests;

public class BasketServiceTests
{
    private readonly Mock<ICatalogService> _mockCatalogService;
    private readonly Mock<ICacheService> _mockCacheService;
    private readonly Mock<ILogger<BasketService>> _mockLogger;
    private readonly BasketService _service;

    public BasketServiceTests()
    {
        _mockCatalogService = new Mock<ICatalogService>();
        _mockCacheService = new Mock<ICacheService>();
        _mockLogger = new Mock<ILogger<BasketService>>();

        _service = new BasketService(_mockCatalogService.Object, _mockCacheService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetBasket_ReturnsBasket_WhenBasketExists()
    {
        // Arrange
        var userId = "testUser";
        var expectedBasket = new Domain.Models.Basket { UserId = userId };
        _mockCacheService.Setup(c => c.Get(userId)).ReturnsAsync(JsonConvert.SerializeObject(expectedBasket));

        // Act
        var result = await _service.GetBasket(userId);

        // Assert
        Assert.Equal(expectedBasket.UserId, result.UserId);
    }

    [Fact]
    public async Task GetBasket_ThrowsArgumentException_WhenUserIdIsNullOrEmpty()
    {
        // Arrange
        var userId = string.Empty; 

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.GetBasket(userId));
    }

    [Fact]
    public async Task AddItem_ReturnsBasketItem_WhenItemExists()
    {
        // Arrange
        var userId = "testUser";
        var itemId = 1;
        var expectedItem = new CatalogItem { Id = itemId };
        _mockCatalogService.Setup(c => c.GetItemById(itemId)).ReturnsAsync(expectedItem);

        var basket = new Domain.Models.Basket { UserId = userId };
        _mockCacheService.Setup(c => c.Get(userId)).ReturnsAsync(JsonConvert.SerializeObject(basket));

        // Act
        var result = await _service.AddItem(userId, itemId);

        // Assert
        Assert.Equal(expectedItem.Id, result.ItemId);
    }

    [Fact]
    public async Task AddItem_ThrowsArgumentException_WhenItemDoesNotExist()
    {
        // Arrange
        var userId = "testUser";
        var itemId = 1;
        _mockCatalogService.Setup(c => c.GetItemById(itemId)).ReturnsAsync((CatalogItem)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.AddItem(userId, itemId));
    }

    [Fact]
    public async Task RemoveItem_ReturnsBasketItem_WhenItemExistsInBasket()
    {
        // Arrange
        var userId = "testUser";
        var itemId = 1;
        var basket = new Domain.Models.Basket { UserId = userId, Items = new List<BasketItem> { new() { ItemId = itemId, Quantity = 2 } } };
        _mockCacheService.Setup(c => c.Get(userId)).ReturnsAsync(JsonConvert.SerializeObject(basket));

        // Act
        var result = await _service.RemoveItem(userId, itemId);

        // Assert
        Assert.Equal(itemId, result.ItemId);
    }

    [Fact]
    public async Task RemoveItem_ThrowsArgumentException_WhenItemDoesNotExistInBasket()
    {
        // Arrange
        var userId = "testUser";
        var itemId = 1;
        var basket = new Domain.Models.Basket { UserId = userId, Items = new List<BasketItem>() };
        _mockCacheService.Setup(c => c.Get(userId)).ReturnsAsync(JsonConvert.SerializeObject(basket));

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.RemoveItem(userId, itemId));
    }

    [Fact]
    public async Task DeleteBasket_ReturnsTrue_WhenBasketExists()
    {
        // Arrange
        var userId = "testUser";
        _mockCacheService.Setup(c => c.Delete(userId)).ReturnsAsync(true);

        // Act
        var result = await _service.DeleteBasket(userId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteBasket_ThrowsArgumentException_WhenUserIdIsNullOrEmpty()
    {
        // Arrange
        var userId = string.Empty; 

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.DeleteBasket(userId));
    }
}
