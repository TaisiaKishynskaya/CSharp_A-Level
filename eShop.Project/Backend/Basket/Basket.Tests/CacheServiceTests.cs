namespace Basket.Tests;

public class CacheServiceTests
{
    private readonly Mock<IDatabase> _mockDatabase;
    private readonly CacheService _service;

    public CacheServiceTests()
    {
        _mockDatabase = new Mock<IDatabase>();
        var mockRedis = new Mock<IConnectionMultiplexer>();

        mockRedis.Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(_mockDatabase.Object);

        _service = new CacheService(mockRedis.Object);
    }

    [Fact]
    public async Task Get_ReturnsValue_WhenKeyExists()
    {
        // Arrange
        string key = "key";
        string value = "value";
        _mockDatabase.Setup(d => d.StringGetAsync(key, It.IsAny<CommandFlags>())).ReturnsAsync(value);

        // Act
        var result = await _service.Get(key);

        // Assert
        Assert.Equal(value, result);
    }

    [Fact]
    public async Task Get_ThrowsException_WhenErrorOccurs()
    {
        // Arrange
        string key = "key";
        _mockDatabase.Setup(d => d.StringGetAsync(key, It.IsAny<CommandFlags>())).ThrowsAsync(new Exception());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.Get(key));
    }

    [Fact]
    public async Task Set_SetsValue_WhenCalled()
    {
        // Arrange
        string key = "testKey";
        string value = "testValue";
        _mockDatabase.Setup(db => db.StringSetAsync(key, value, null, It.IsAny<When>(), It.IsAny<CommandFlags>())).ReturnsAsync(true);

        // Act
        var result = await _service.Set(key, value);

        // Assert
        Assert.True(result);
        _mockDatabase.Verify(d => d.StringSetAsync(key, value, null, It.IsAny<When>(), It.IsAny<CommandFlags>()), Times.Once);
    }

    [Fact]
    public async Task Set_ThrowsException_WhenErrorOccurs()
    {
        // Arrange
        string key = "key";
        string value = "value";
        _mockDatabase.Setup(d => d.StringSetAsync(key, value, null, It.IsAny<When>(), It.IsAny<CommandFlags>())).ThrowsAsync(new Exception());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.Set(key, value));
        _mockDatabase.Verify(d => d.StringSetAsync(key, value, null, It.IsAny<When>(), It.IsAny<CommandFlags>()), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnsTrue_WhenKeyExists()
    {
        // Arrange
        string key = "key";
        _mockDatabase.Setup(d => d.KeyDeleteAsync(key, It.IsAny<CommandFlags>())).ReturnsAsync(true);

        // Act
        var result = await _service.Delete(key);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Delete_ThrowsException_WhenErrorOccurs()
    {
        // Arrange
        string key = "key";
        _mockDatabase.Setup(d => d.KeyDeleteAsync(key, It.IsAny<CommandFlags>())).ThrowsAsync(new Exception());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.Delete(key));
    }
}
