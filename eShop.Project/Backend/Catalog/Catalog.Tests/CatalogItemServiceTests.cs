namespace Catalog.Tests;

public class CatalogItemServiceTests
{
    private readonly Mock<ICatalogItemRepository<CatalogItemEntity>> _mockRepo;
    private readonly Mock<ICatalogTypeService> _mockTypeService;
    private readonly Mock<ICatalogBrandService> _mockBrandService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<CatalogItemService>> _mockLogger;
    private readonly Fixture _fixture = new();

    private readonly CatalogItemService _service;

    private readonly List<CatalogItemEntity> _catalogItemEntities;

    public CatalogItemServiceTests()
    {
        _mockRepo = new Mock<ICatalogItemRepository<CatalogItemEntity>>();
        _mockBrandService = new Mock<ICatalogBrandService>();
        _mockTypeService = new Mock<ICatalogTypeService>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<CatalogItemService>>();

        _service = new CatalogItemService(
            _mockRepo.Object, 
            _mockBrandService.Object,
            _mockTypeService.Object, 
            _mockMapper.Object,
            _mockLogger.Object);

        _catalogItemEntities = new()
        {
             new CatalogItemEntity
            {
                Id = 1,
                Title = "Item1",
                Description = "Description1",
                Price = 100,
                PictureFile = "1.png",
                TypeId = 1,
                BrandId = 1,
                Quantity = 5,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            },

            new CatalogItemEntity
            {
                Id = 2,
                Title = "Item2",
                Description = "Description2",
                Price = 120,
                PictureFile = "2.png",
                TypeId = 1,
                BrandId = 1,
                Quantity = 10,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            }
        };
    }

    [Fact]
    public async Task Get_ReturnsItems_WhenItemsExist()
    {
        // Arrange
        var catalogItemEntities = _fixture.CreateMany<CatalogItemEntity>(2).ToList();
        var expectedCatalogItems = _fixture.CreateMany<CatalogItem>(2).ToList();

        _mockRepo.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(catalogItemEntities);
        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<CatalogItem>>(catalogItemEntities)).Returns(expectedCatalogItems);

        // Act
        var result = await _service.Get(1, 10);

        // Assert
        Assert.Equal(expectedCatalogItems, result);
        _mockRepo.Verify(repo => repo.Get(1, 10), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<IEnumerable<CatalogItem>>(catalogItemEntities), Times.Once);
    }

    [Fact]
    public async Task Get_ThrowsNotFoundException_WhenItemsDoNotExist()
    {
        // Arrange
        var page = 1;
        var size = 2;
        _mockRepo.Setup(repo => repo.Get(page, size)).ReturnsAsync((List<CatalogItemEntity>)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.Get(page, size));
        _mockRepo.Verify(repo => repo.Get(page, size), Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnsItem_WhenItemExists()
    {
        // Arrange
        var catalogItemEntity = _fixture.Create<CatalogItemEntity>();
        var expectedCatalogItem = _fixture.Create<CatalogItem>();

        _mockRepo.Setup(repo => repo.GetById(catalogItemEntity.Id)).ReturnsAsync(catalogItemEntity);
        _mockMapper.Setup(mapper => mapper.Map<CatalogItem>(catalogItemEntity)).Returns(expectedCatalogItem);

        // Act
        var result = await _service.GetById(catalogItemEntity.Id);

        // Assert
        Assert.Equal(expectedCatalogItem, result);
        _mockRepo.Verify(repo => repo.GetById(catalogItemEntity.Id), Times.Once);
    }

    [Fact]
    public async Task GetById_ThrowsNotFoundException_WhenItemDoesNotExist()
    {
        // Arrange
        var id = _fixture.Create<int>();
        _mockRepo.Setup(repo => repo.GetById(id)).ReturnsAsync((CatalogItemEntity)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetById(id));
        _mockRepo.Verify(repo => repo.GetById(id), Times.Once);
    }

    [Fact]
    public async Task Add_ValidItem_ReturnsItemId()
    {
        // Arrange
        var newItem = new CatalogItem
        {
            Id = 3,
            Title = "Item3",
            Description = "Description3",
            Price = 130,
            PictureFile = "3.png",
            Quantity = 15,
            Type = new CatalogType { Title = "Type1" },
            Brand = new CatalogBrand { Title = "Brand1" }
        };

        var existingType = new CatalogType { Id = 1, Title = "Type1" };
        var existingBrand = new CatalogBrand { Id = 1, Title = "Brand1" };

        _mockRepo.Setup(r => r.GetByPictureFile(It.IsAny<string>())).ReturnsAsync((CatalogItemEntity)null);
        _mockRepo.Setup(r => r.GetByTitle(It.IsAny<string>())).ReturnsAsync((CatalogItemEntity)null);
        _mockTypeService.Setup(s => s.GetByTitle(It.IsAny<string>())).ReturnsAsync(existingType);
        _mockBrandService.Setup(s => s.GetByTitle(It.IsAny<string>())).ReturnsAsync(existingBrand);
        _mockRepo.Setup(r => r.Add(It.IsAny<CatalogItemEntity>())).ReturnsAsync(3);

        // Act
        var result = await _service.Add(newItem);

        // Assert
        Assert.Equal(3, result);
        _mockRepo.Verify(r => r.GetByPictureFile(newItem.PictureFile), Times.Once);
        _mockRepo.Verify(r => r.GetByTitle(newItem.Title), Times.Once);
        _mockTypeService.Verify(s => s.GetByTitle(newItem.Type.Title), Times.Once);
        _mockBrandService.Verify(s => s.GetByTitle(newItem.Brand.Title), Times.Once);
        _mockRepo.Verify(r => r.Add(It.Is<CatalogItemEntity>(i => i.Title == newItem.Title)), Times.Once);
    }

    [Fact]
    public async Task Add_ThrowsValidationException_WhenItemIsNotUnique()
    {
        // Arrange
        var catalogItem = _fixture.Create<CatalogItem>();
        var catalogItemEntity = _fixture.Create<CatalogItemEntity>();

        _mockRepo.Setup(repo => repo.GetByPictureFile(catalogItem.PictureFile)).ReturnsAsync(catalogItemEntity);
        _mockMapper.Setup(mapper => mapper.Map<CatalogItemEntity>(catalogItem)).Returns(catalogItemEntity);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _service.Add(catalogItem));
        _mockRepo.Verify(repo => repo.GetByPictureFile(catalogItem.PictureFile), Times.Once);
    }

    [Fact]
    public async Task Update_ValidItem_ReturnsItemId()
    {
        // Arrange
        var newItem = _fixture.Create<CatalogItem>();
        var existingItemEntity = _fixture.Create<CatalogItemEntity>();
        existingItemEntity.Id = newItem.Id; // Ensure the existing item has the same Id as the new item
        var existingType = _fixture.Create<CatalogType>();
        var existingBrand = _fixture.Create<CatalogBrand>();

        _mockRepo.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(existingItemEntity);
        _mockTypeService.Setup(s => s.GetByTitle(It.IsAny<string>())).ReturnsAsync(existingType);
        _mockBrandService.Setup(s => s.GetByTitle(It.IsAny<string>())).ReturnsAsync(existingBrand);
        _mockRepo.Setup(r => r.GetByPictureFile(It.IsAny<string>())).ReturnsAsync((CatalogItemEntity)null);
        _mockRepo.Setup(r => r.Update(It.IsAny<CatalogItemEntity>())).ReturnsAsync(newItem.Id);

        // Act
        var result = await _service.Update(newItem);

        // Assert
        Assert.Equal(newItem.Id, result);
        _mockRepo.Verify(r => r.GetById(newItem.Id), Times.Once);
        _mockTypeService.Verify(s => s.GetByTitle(newItem.Type.Title), Times.Once);
        _mockBrandService.Verify(s => s.GetByTitle(newItem.Brand.Title), Times.Once);
        _mockRepo.Verify(r => r.Update(It.Is<CatalogItemEntity>(i => i.Id == existingItemEntity.Id)), Times.Once);
    }

    [Fact]
    public async Task Update_ItemNotFound_ThrowsNotFoundException()
    {
        // Arrange
        var newItem = _fixture.Create<CatalogItem>();

        _mockRepo.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((CatalogItemEntity)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _service.Update(newItem));
        Assert.Equal($"Item with id = {newItem.Id} not found", exception.Message);
        _mockRepo.Verify(r => r.GetById(newItem.Id), Times.Once);
    }

    [Fact]
    public async Task Delete_ValidId_ReturnsId()
    {
        // Arrange
        var existingItemEntity = _fixture.Create<CatalogItemEntity>();
        int id = existingItemEntity.Id;

        _mockRepo.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(existingItemEntity);
        _mockRepo.Setup(r => r.Delete(It.IsAny<int>())).ReturnsAsync(id);

        // Act
        var result = await _service.Delete(id);

        // Assert
        Assert.Equal(id, result);
        _mockRepo.Verify(r => r.GetById(id), Times.Once);
        _mockRepo.Verify(r => r.Delete(id), Times.Once);
    }

    [Fact]
    public async Task Delete_ItemNotFound_ThrowsNotFoundException()
    {
        // Arrange
        int id = 1;

        _mockRepo.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((CatalogItemEntity)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _service.Delete(id));
        Assert.Equal($"Item with id = {id} not found", exception.Message);
        _mockRepo.Verify(r => r.GetById(id), Times.Once);
    }

    [Fact]
    public async Task Count_ReturnsItemCount()
    {
        // Arrange
        int itemCount = 5;
        _mockRepo.Setup(r => r.Count()).ReturnsAsync(itemCount);

        // Act
        var result = await _service.Count();

        // Assert
        Assert.Equal(itemCount, result);
        _mockRepo.Verify(r => r.Count(), Times.Once);
    }

    [Fact]
    public async Task Count_ThrowsException()
    {
        // Arrange
        _mockRepo.Setup(r => r.Count()).ThrowsAsync(new Exception());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.Count());
        _mockRepo.Verify(r => r.Count(), Times.Once);
    }
}


