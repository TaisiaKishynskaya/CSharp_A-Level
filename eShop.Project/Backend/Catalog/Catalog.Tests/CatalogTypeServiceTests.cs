namespace Catalog.Tests;

public class CatalogTypeServiceTests
{
    private readonly Mock<ICatalogTypeRepository<CatalogTypeEntity>> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<CatalogTypeService>> _mockLogger;

    private readonly CatalogTypeService _service;

    private readonly List<CatalogTypeEntity> _catalogTypeEntities;

    public CatalogTypeServiceTests()
    {
        _mockRepo = new();
        _mockMapper = new();
        _mockLogger = new();

        _service = new CatalogTypeService(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);

        _catalogTypeEntities = new()
        {
            new CatalogTypeEntity { Id = 1, Title = "Type1", CreatedAt = DateTime.UtcNow, UpdatedAt = null},
            new CatalogTypeEntity { Id = 2, Title = "Type2", CreatedAt = DateTime.UtcNow, UpdatedAt = null}
        };
    }

    [Fact]
    public async Task Get_ReturnsMappedCatalogTypes_WhenRepositoryReturnsEntities()
    {
        //Arrange
        _mockRepo.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_catalogTypeEntities);

        var expectedCatalogTypes = new List<CatalogType>
        {
            new() { Id = 1, Title = "Type1", CreatedAt = DateTime.UtcNow, UpdatedAt = null},
            new() { Id = 2, Title = "Type2", CreatedAt = DateTime.UtcNow, UpdatedAt = null}
        };

        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<CatalogType>>(_catalogTypeEntities)).Returns(expectedCatalogTypes);

        //Act
        var result = await _service.Get(1, 10);

        //Assert
        Assert.Equal(expectedCatalogTypes, result);
        _mockRepo.Verify(repo => repo.Get(1, 10), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<IEnumerable<CatalogType>>(_catalogTypeEntities), Times.Once);
    }

    [Fact]
    public async Task Get_ThrowsNotFoundException_WhenRepositoryReturnsNull()
    {
        //Arrange
        _mockRepo.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((List<CatalogTypeEntity>)null);

        //Act
        await Assert.ThrowsAsync<NotFoundException>(() => _service.Get(1, 10));

        //Assert
        _mockRepo.Verify(repo => repo.Get(1,10), Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnsMappedCatalogTypes_WhenRepositoryReturnsEntities()
    {
        //Arrange
        var catalogTypeEntity = _catalogTypeEntities[0];
        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(catalogTypeEntity);

        var expectedCatalogType = new CatalogType { Id = 1, Title = "Type1", CreatedAt = DateTime.UtcNow, UpdatedAt = null };
        _mockMapper.Setup(mapper => mapper.Map<CatalogType>(catalogTypeEntity)).Returns(expectedCatalogType);

        //Act
        var result = await _service.GetById(1);

        //Assert
        Assert.Equal(expectedCatalogType, result);
        _mockRepo.Verify(repo => repo.GetById(1), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<CatalogType>(catalogTypeEntity), Times.Once);
    }

    [Fact]
    public async Task GetById_ThrowsNotFoundException_WhenRepositoryReturnsNull()
    {
        //Arrange
        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((CatalogTypeEntity)null);

        //Act
        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetById(1));

        //Assert
        _mockRepo.Verify(repo => repo.GetById(1), Times.Once);
    }

    [Fact]
    public async Task Add_ReturnsId_WhenTypeIsUnique()
    {
        //Arrange
        var catalogType = new CatalogType { Id = 3, Title = "Type3", CreatedAt = DateTime.UtcNow, UpdatedAt = null };
        var catalogTypeEntity = new CatalogTypeEntity { Id = 3, Title = "Type3", CreatedAt = DateTime.UtcNow, UpdatedAt = null };

        _mockRepo.Setup(repo => repo.GetByTitle(It.IsAny<string>())).ReturnsAsync((CatalogTypeEntity)null);
        _mockRepo.Setup(repo => repo.Add(It.IsAny<CatalogTypeEntity>())).ReturnsAsync(3);
        _mockMapper.Setup(mapper => mapper.Map<CatalogTypeEntity>(catalogType)).Returns(catalogTypeEntity);

        //Act
        var result = await _service.Add(catalogType);

        //Assert
        Assert.Equal(3, result);
        _mockRepo.Verify(repo => repo.GetByTitle("Type3"), Times.Once);
        _mockRepo.Verify(repo => repo.Add(catalogTypeEntity), Times.Once);
    }

    [Fact]
    public async Task Add_ThrowsValidationAsyncException_WhenTypeIsNotUnique()
    {
        //Arrange
        var catalogType = new CatalogType { Id = 1, Title = "Type1", CreatedAt = DateTime.UtcNow, UpdatedAt = null };
        var catalogTypeEntity = new CatalogTypeEntity { Id = 1, Title = "Type1", CreatedAt = DateTime.UtcNow, UpdatedAt = null };

        _mockRepo.Setup(repo => repo.GetByTitle(It.IsAny<string>())).ReturnsAsync(catalogTypeEntity);
        _mockMapper.Setup(mapper => mapper.Map<CatalogTypeEntity>(catalogType)).Returns(catalogTypeEntity);

        //Act
        await Assert.ThrowsAsync<ValidationAsyncException>(() => _service.Add(catalogType));

        //Assert
        _mockRepo.Verify(repo => repo.GetByTitle("Type1"), Times.Once);
    }

    [Fact]
    public async Task Update_ReturnsId_WhenTypeExists()
    {
        //Arrange
        var catalogType = new CatalogType { Id = 1, Title = "Type1", CreatedAt = DateTime.UtcNow, UpdatedAt = null };
        var catalogTypeEntity = new CatalogTypeEntity { Id = 1, Title = "Type1", CreatedAt = DateTime.UtcNow, UpdatedAt = null };

        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(catalogTypeEntity);
        _mockRepo.Setup(repo => repo.Update(It.IsAny<CatalogTypeEntity>())).ReturnsAsync(1);
        _mockMapper.Setup(mapper => mapper.Map(catalogType, catalogTypeEntity)).Returns(catalogTypeEntity);

        //Act
        var result = await _service.Update(catalogType);

        //Assert
        Assert.Equal(1, result);
        _mockRepo.Verify(repo => repo.GetById(1), Times.Once);
        _mockRepo.Verify(repo => repo.Update(catalogTypeEntity), Times.Once);
    }

    [Fact]
    public async Task Update_ThrowsNotFoundException_WhenTypeDoesNotExist()
    {
        //Arrange
        var catalogType = new CatalogType { Id = 1, Title = "Type1", CreatedAt = DateTime.UtcNow, UpdatedAt = null };

        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((CatalogTypeEntity)null);

        //Act
        await Assert.ThrowsAsync<NotFoundException>(() => _service.Update(catalogType));

        //Assert
        _mockRepo.Verify(repo => repo.GetById(1), Times.Once);
    }

    [Fact]
    public async Task Delete_ReturnsZero_WhenTypeExists()
    {
        // Arrange
        var id = 1;
        _mockRepo.Setup(repo => repo.GetById(id)).ReturnsAsync(new CatalogTypeEntity { Id = id });
        _mockRepo.Setup(repo => repo.Delete(id)).ReturnsAsync(0);

        // Act
        var result = await _service.Delete(id);

        // Assert
        Assert.Equal(0, result);
        _mockRepo.Verify(repo => repo.Delete(id), Times.Once);
    }

    [Fact]
    public async Task Delete_ThrowsNotFoundException_WhenTypeDoesNotExist()
    {
        // Arrange
        var id = 1;
        _mockRepo.Setup(repo => repo.GetById(id)).ReturnsAsync((CatalogTypeEntity)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.Delete(id));
        _mockRepo.Verify(repo => repo.GetById(id), Times.Once);
    }

    [Fact]
    public async Task Count_ReturnsCorrectNumber()
    {
        // Arrange
        var count = 2;
        _mockRepo.Setup(repo => repo.Count()).ReturnsAsync(count);

        // Act
        var result = await _service.Count();

        // Assert
        Assert.Equal(count, result);
        _mockRepo.Verify(repo => repo.Count(), Times.Once);
    }

    [Fact]
    public async Task Count_ThrowsException_WhenRepositoryThrowsException()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.Count()).ThrowsAsync(new Exception());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.Count());
        _mockRepo.Verify(repo => repo.Count(), Times.Once);
    }

    // Тесты для метода GetByTitle
    [Fact]
    public async Task GetByTitle_ReturnsCorrectType()
    {
        // Arrange
        var title = "Type1";
        var catalogTypeEntity = new CatalogTypeEntity { Id = 1, Title = title };
        var catalogType = new CatalogType { Id = 1, Title = title };

        _mockRepo.Setup(repo => repo.GetByTitle(title)).ReturnsAsync(catalogTypeEntity);
        _mockMapper.Setup(mapper => mapper.Map<CatalogType>(catalogTypeEntity)).Returns(catalogType);

        // Act
        var result = await _service.GetByTitle(title);

        // Assert
        Assert.Equal(catalogType, result);
        _mockRepo.Verify(repo => repo.GetByTitle(title), Times.Once);
    }

    [Fact]
    public async Task GetByTitle_ReturnsNull_WhenTitleDoesNotExist()
    {
        // Arrange
        var title = "NonExistentTitle";
        _mockRepo.Setup(repo => repo.GetByTitle(title)).ReturnsAsync((CatalogTypeEntity)null);

        // Act
        var result = await _service.GetByTitle(title);

        // Assert
        Assert.Null(result);
        _mockRepo.Verify(repo => repo.GetByTitle(title), Times.Once);
    }
}
