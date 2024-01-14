namespace Catalog.UnitTests;

public class CatalogTypeServiceTest
{
    private readonly ICatalogTypeService _catalogTypeService;
    private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
    private readonly Mock<ILogger<CatalogTypeService>> _logger;

    private readonly List<CatalogType> _catalogTypes = new List<CatalogType>
    {
        new CatalogType { Id = 1, Type = "Type1" },
        new CatalogType { Id = 2, Type = "Type2" }
    };

    public CatalogTypeServiceTest()
    {
        _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
        _logger = new Mock<ILogger<CatalogTypeService>>();

        _catalogTypeService = new CatalogTypeService(_catalogTypeRepository.Object, _logger.Object);
    }

    [Fact]
    public async Task Get_ReturnsPaginatedItems_WhenSuccessful()
    {
        // Arrange
        _catalogTypeRepository.Setup(repo => repo.Get()).ReturnsAsync(_catalogTypes);

        // Act
        var result = await _catalogTypeService.Get(1, 1);

        // Assert
        Assert.Single(result.Data);
        Assert.Equal(1, result.PageIndex);
        Assert.Equal(1, result.PageSize);
        Assert.Equal(result.Data.Count(), result.count);
    }

    [Fact]
    public async Task Get_ReturnsEmpty_WhenNoData()
    {
        // Arrange
        _catalogTypeRepository.Setup(repo => repo.Get()).ReturnsAsync(new List<CatalogType>());

        // Act
        var result = await _catalogTypeService.Get(1, 1);

        // Assert
        Assert.Empty(result.Data);
        Assert.Equal(1, result.PageIndex);
        Assert.Equal(1, result.PageSize);
        Assert.Equal(0, result.count);
    }

    [Fact]
    public async Task Add_ReturnsNewId_WhenSuccessful()
    {
        // Arrange
        var catalogTypeRequest = new CatalogTypeRequest { TypeName = "NewType" };
        _catalogTypeRepository.Setup(repo => repo.GetByName(catalogTypeRequest.TypeName)).ReturnsAsync((CatalogType)null);
        _catalogTypeRepository.Setup(repo => repo.Add(It.IsAny<CatalogType>())).ReturnsAsync(3);

        // Act
        var result = await _catalogTypeService.Add(catalogTypeRequest);

        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public async Task Add_ThrowsException_WhenTypeAlreadyExists()
    {
        // Arrange
        var existingType = new CatalogType { Id = 1, Type = "ExistingType" };
        var catalogTypeRequest = new CatalogTypeRequest { TypeName = existingType.Type };
        _catalogTypeRepository.Setup(repo => repo.GetByName(catalogTypeRequest.TypeName)).ReturnsAsync(existingType);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _catalogTypeService.Add(catalogTypeRequest));
        Assert.Equal($"Type with name {catalogTypeRequest.TypeName} already exists.", exception.Message);
    }

    [Fact]
    public async Task Update_ReturnsUpdatedType_WhenSuccessful()
    {
        // Arrange
        var existingType = new CatalogType { Id = 1, Type = "ExistingType" };
        var updatedType = new CatalogType { Id = 1, Type = "UpdatedType" };
        _catalogTypeRepository.Setup(repo => repo.GetById(existingType.Id)).ReturnsAsync(existingType);
        _catalogTypeRepository.Setup(repo => repo.Update(updatedType)).Returns(Task.CompletedTask);

        // Act
        var result = await _catalogTypeService.Update(updatedType);

        // Assert
        Assert.Equal(updatedType.Id, result.Id);
        Assert.Equal(updatedType.Type, result.Type);
    }

    [Fact]
    public async Task Update_ThrowsException_WhenTypeDoesNotExist()
    {
        // Arrange
        var nonExistingType = new CatalogType { Id = 3, Type = "NonExistingType" };
        _catalogTypeRepository.Setup(repo => repo.GetById(nonExistingType.Id)).ReturnsAsync((CatalogType)null);

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _catalogTypeService.Update(nonExistingType));

        // Assert
        Assert.Equal($"Type with id {nonExistingType.Id} does not exist.", exception.Message);
    }

    [Fact]
    public async Task Delete_ReturnsId_WhenSuccessful()
    {
        // Arrange
        var existingType = new CatalogType { Id = 1, Type = "ExistingType" };
        _catalogTypeRepository.Setup(repo => repo.GetById(existingType.Id)).ReturnsAsync(existingType);
        _catalogTypeRepository.Setup(repo => repo.Delete(existingType.Id)).Returns(Task.CompletedTask);

        // Act
        var result = await _catalogTypeService.Delete(existingType.Id);

        // Assert
        Assert.Equal(existingType.Id, result);
    }

    [Fact]
    public async Task Delete_ThrowsException_WhenTypeDoesNotExist()
    {
        // Arrange
        var nonExistingTypeId = 3;
        _catalogTypeRepository.Setup(repo => repo.GetById(nonExistingTypeId)).ReturnsAsync((CatalogType)null);

        // Act 
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _catalogTypeService.Delete(nonExistingTypeId));

        // Assert
        Assert.Equal($"Type with id {nonExistingTypeId} does not exist.", exception.Message);
    }
}