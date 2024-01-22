using Microsoft.AspNetCore.Hosting;

namespace Catalog.UnitTests;

public class CatalogItemServiceTest
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<ILogger<CatalogItemService>> _logger;
    private readonly Mock<IWebHostEnvironment> _env;

    private readonly List<CatalogItem> _catalogItems = new List<CatalogItem>
    {
        new CatalogItem
        {
            Id = 1,
            Name = "Item1",
            Description = "Description1",
            Price = 10.0m,
            PictureUri = "1.png",
            CatalogTypeId = 1,
            CatalogType = new CatalogType { Id = 1, Type = "Type1" },
            CatalogBrandId = 1,
            CatalogBrand = new CatalogBrand { Id = 1, Brand = "Brand1" },
            AvailableStock = 100
        },
        new CatalogItem
        {
            Id = 2,
            Name = "Item2",
            Description = "Description2",
            Price = 20.0m,
            PictureUri = "2.png",
            CatalogTypeId = 2,
            CatalogType = new CatalogType { Id = 2, Type = "Type2" },
            CatalogBrandId = 2,
            CatalogBrand = new CatalogBrand { Id = 2, Brand = "Brand2" },
            AvailableStock = 200
        }
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _logger = new Mock<ILogger<CatalogItemService>>();
        _env = new Mock<IWebHostEnvironment>();

        _catalogItemService = new CatalogItemService(_catalogItemRepository.Object, _logger.Object, _env.Object);
    }


    [Fact]
    public async Task Get_ReturnsPaginatedItems_WhenSuccessful()
    {
        // Arrange
        _catalogItemRepository.Setup(repo => repo.Get()).ReturnsAsync(_catalogItems);

        // Act
        var result = await _catalogItemService.Get(1, 1);

        // Assert
        Assert.Single(result.Data);
        Assert.Equal(1, result.PageIndex);
        Assert.Equal(1, result.PageSize);
        Assert.Equal(_catalogItems.Count, result.count);
    }

    [Fact]
    public async Task Get_ReturnsEmpty_WhenNoData()
    {
        // Arrange
        _catalogItemRepository.Setup(repo => repo.Get()).ReturnsAsync(new List<CatalogItem>());

        // Act
        var result = await _catalogItemService.Get(1, 1);

        // Assert
        Assert.Empty(result.Data);
        Assert.Equal(1, result.PageIndex);
        Assert.Equal(1, result.PageSize);
        Assert.Equal(0, result.count);
    }

    [Fact]
    public async Task GetById_ReturnsItem_WhenItemExists()
    {
        // Arrange
        var existingItem = _catalogItems.First();
        _catalogItemRepository.Setup(repo => repo.GetById(existingItem.Id)).ReturnsAsync(existingItem);

        // Act
        var result = await _catalogItemService.GetById(existingItem.Id);

        // Assert
        Assert.Equal(existingItem.Id, result.Id);
        Assert.Equal(existingItem.Name, result.Name);
    }

    [Fact]
    public async Task GetById_ThrowsException_WhenItemDoesNotExist()
    {
        // Arrange
        var nonExistingItemId = 999;
        _catalogItemRepository.Setup(repo => repo.GetById(nonExistingItemId)).ReturnsAsync((CatalogItem)null);

        // Act 
        var exception = await Assert.ThrowsAsync<Exception>(() => _catalogItemService.GetById(nonExistingItemId));

        // Assert
        Assert.Equal($"Item with id {nonExistingItemId} does not exist.", exception.Message);
    }

    [Fact]
    public async Task GetPicturePathById_ReturnsImagePath_WhenImageExists()
    {
        // Arrange
        var existingItem = _catalogItems.First();
        _catalogItemRepository.Setup(repo => repo.GetPictureUriById(existingItem.Id)).ReturnsAsync(existingItem.PictureUri);
        _env.Setup(env => env.ContentRootPath).Returns("rootPath");
        var expectedPath = Path.Combine("rootPath", "Pics", existingItem.PictureUri);
        Directory.CreateDirectory(Path.GetDirectoryName(expectedPath));
        File.Create(expectedPath).Dispose();

        // Act
        var result = await _catalogItemService.GetPicturePathById(existingItem.Id);

        // Assert
        Assert.Equal(expectedPath, result);

        // Cleanup
        File.Delete(expectedPath);
    }

    [Fact]
    public async Task GetPicturePathById_ThrowsException_WhenImageDoesNotExist()
    {
        // Arrange
        var nonExistingItemId = 999;
        _catalogItemRepository.Setup(repo => repo.GetPictureUriById(nonExistingItemId)).ReturnsAsync((string)null);

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _catalogItemService.GetPicturePathById(nonExistingItemId));

        // Assert
        Assert.Equal($"Item with id {nonExistingItemId} does not exist.", exception.Message);
    }

    [Fact]
    public async Task GetPictureUriById_ReturnsPictureUri_WhenItemExists()
    {
        // Arrange
        var existingItem = _catalogItems.First();
        _catalogItemRepository.Setup(repo => repo.GetPictureUriById(existingItem.Id)).ReturnsAsync(existingItem.PictureUri);

        // Act
        var result = await _catalogItemService.GetPictureUriById(existingItem.Id);

        // Assert
        Assert.Equal(Path.Combine("Pics", existingItem.PictureUri), result);
    }

    [Fact]
    public async Task GetPictureUriById_ThrowsException_WhenItemDoesNotExist()
    {
        // Arrange
        var nonExistingItemId = 999;
        _catalogItemRepository.Setup(repo => repo.GetPictureUriById(nonExistingItemId)).ReturnsAsync((string)null);

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _catalogItemService.GetPictureUriById(nonExistingItemId));

        // Assert
        Assert.Equal($"Item with id {nonExistingItemId} does not exist.", exception.Message);
    }

    [Fact]
    public async Task Add_ReturnsNewId_WhenSuccessful()
    {
        // Arrange
        var catalogItemRequest = new CatalogItemRequest { Name = "NewItem", Price = 30.0m };
        _catalogItemRepository.Setup(repo => repo.Add(It.IsAny<CatalogItem>())).ReturnsAsync(3);

        // Act
        var result = await _catalogItemService.Add(catalogItemRequest);

        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public async Task Add_ThrowsException_WhenItemAlreadyExists()
    {
        // Arrange
        var existingItem = _catalogItems.First();
        var catalogItemRequest = new CatalogItemRequest { Name = existingItem.Name, Price = existingItem.Price };
        _catalogItemRepository.Setup(repo => repo.Add(It.IsAny<CatalogItem>())).Throws(new Exception());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _catalogItemService.Add(catalogItemRequest));
    }

    [Fact]
    public async Task Update_ReturnsUpdatedItem_WhenSuccessful()
    {
        // Arrange
        var existingItem = _catalogItems.First();
        existingItem.Name = "UpdatedItem";
        _catalogItemRepository.Setup(repo => repo.GetById(existingItem.Id)).ReturnsAsync(existingItem);
        _catalogItemRepository.Setup(repo => repo.Update(existingItem)).Returns(Task.CompletedTask);

        // Act
        var result = await _catalogItemService.Update(existingItem);

        // Assert
        Assert.Equal(existingItem.Id, result.Id);
        Assert.Equal(existingItem.Name, result.Name);
    }

    [Fact]
    public async Task Update_ThrowsException_WhenItemDoesNotExist()
    {
        // Arrange
        var nonExistingItem = new CatalogItem { Id = 999, Name = "NonExistingItem", Price = 999.0m };
        _catalogItemRepository.Setup(repo => repo.GetById(nonExistingItem.Id)).ReturnsAsync((CatalogItem)null);

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _catalogItemService.Update(nonExistingItem));

        // Assert
        Assert.Equal($"Item with id {nonExistingItem.Id} does not exist.", exception.Message);
    }

    [Fact]
    public async Task Delete_ReturnsId_WhenSuccessful()
    {
        // Arrange
        var existingItem = _catalogItems.First();
        _catalogItemRepository.Setup(repo => repo.GetById(existingItem.Id)).ReturnsAsync(existingItem);
        _catalogItemRepository.Setup(repo => repo.Delete(existingItem.Id)).Returns(Task.CompletedTask);

        // Act
        var result = await _catalogItemService.Delete(existingItem.Id);

        // Assert
        Assert.Equal(existingItem.Id, result);
    }

    [Fact]
    public async Task Delete_ThrowsException_WhenItemDoesNotExist()
    {
        // Arrange
        var nonExistingItemId = 999;
        _catalogItemRepository.Setup(repo => repo.GetById(nonExistingItemId)).ReturnsAsync((CatalogItem)null);

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _catalogItemService.Delete(nonExistingItemId));

        // Assert
        Assert.Equal($"Item with id {nonExistingItemId} does not exist.", exception.Message);
    }
}
