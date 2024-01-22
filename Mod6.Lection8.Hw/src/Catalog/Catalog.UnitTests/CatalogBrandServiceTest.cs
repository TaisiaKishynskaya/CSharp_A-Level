namespace Catalog.UnitTests;

public class CatalogBrandServiceTest
{
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
    private readonly Mock<ILogger<CatalogBrandService>> _logger;

    private readonly List<CatalogBrand> _catalogBrands = new List<CatalogBrand>
    {
        new CatalogBrand { Id = 1, Brand = "Brand1" },
        new CatalogBrand { Id = 2, Brand = "Brand2" }
    };

    public CatalogBrandServiceTest()
    {
        _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
        _logger = new Mock<ILogger<CatalogBrandService>>();

        _catalogBrandService = new CatalogBrandService(_catalogBrandRepository.Object, _logger.Object);
    }

    [Fact]
    public async Task Get_ReturnsPaginatedItems_WhenSuccessful()
    {
        // Arrange
        _catalogBrandRepository.Setup(repo => repo.Get()).ReturnsAsync(_catalogBrands);

        // Act
        var result = await _catalogBrandService.Get(1, 1);

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
        _catalogBrandRepository.Setup(repo => repo.Get()).ReturnsAsync(new List<CatalogBrand>());

        // Act
        var result = await _catalogBrandService.Get(1, 1);

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
        var catalogBrandRequest = new CatalogBrandRequest { BrandName = "NewBrand" };
        _catalogBrandRepository.Setup(repo => repo.GetByName(catalogBrandRequest.BrandName)).ReturnsAsync((CatalogBrand)null);
        _catalogBrandRepository.Setup(repo => repo.Add(It.IsAny<CatalogBrand>())).ReturnsAsync(3);

        // Act
        var result = await _catalogBrandService.Add(catalogBrandRequest);

        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public async Task Add_ThrowsException_WhenBrandAlreadyExists()
    {
        // Arrange
        var existingBrand = new CatalogBrand { Id = 1, Brand = "ExistingBrand" };
        var catalogBrandRequest = new CatalogBrandRequest { BrandName = existingBrand.Brand };
        _catalogBrandRepository.Setup(repo => repo.GetByName(catalogBrandRequest.BrandName)).ReturnsAsync(existingBrand);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _catalogBrandService.Add(catalogBrandRequest));
        Assert.Equal($"Brand with name {catalogBrandRequest.BrandName} already exists.", exception.Message);
    }

    [Fact]
    public async Task Update_ReturnsUpdatedBrand_WhenSuccessful()
    {
        // Arrange
        var existingBrand = new CatalogBrand { Id = 1, Brand = "ExistingBrand" };
        var updatedBrand = new CatalogBrand { Id = 1, Brand = "UpdatedBrand" };
        _catalogBrandRepository.Setup(repo => repo.GetById(existingBrand.Id)).ReturnsAsync(existingBrand);
        _catalogBrandRepository.Setup(repo => repo.Update(updatedBrand)).Returns(Task.CompletedTask);

        // Act
        var result = await _catalogBrandService.Update(updatedBrand);

        // Assert
        Assert.Equal(updatedBrand.Id, result.Id);
        Assert.Equal(updatedBrand.Brand, result.Brand);
    }

    [Fact]
    public async Task Update_ThrowsException_WhenBrandDoesNotExist()
    {
        // Arrange
        var nonExistingBrand = new CatalogBrand { Id = 3, Brand = "NonExistingBrand" };
        _catalogBrandRepository.Setup(repo => repo.GetById(nonExistingBrand.Id)).ReturnsAsync((CatalogBrand)null);

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _catalogBrandService.Update(nonExistingBrand));

        // Assert
        Assert.Equal($"Brand with id {nonExistingBrand.Id} does not exist.", exception.Message);
    }

    [Fact]
    public async Task Delete_ReturnsId_WhenSuccessful()
    {
        // Arrange
        var existingBrand = new CatalogBrand { Id = 1, Brand = "ExistingBrand" };
        _catalogBrandRepository.Setup(repo => repo.GetById(existingBrand.Id)).ReturnsAsync(existingBrand);
        _catalogBrandRepository.Setup(repo => repo.Delete(existingBrand.Id)).Returns(Task.CompletedTask);

        // Act
        var result = await _catalogBrandService.Delete(existingBrand.Id);

        // Assert
        Assert.Equal(existingBrand.Id, result);
    }

    [Fact]
    public async Task Delete_ThrowsException_WhenBrandDoesNotExist()
    {
        // Arrange
        var nonExistingBrandId = 3;
        _catalogBrandRepository.Setup(repo => repo.GetById(nonExistingBrandId)).ReturnsAsync((CatalogBrand)null);

        // Act 
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _catalogBrandService.Delete(nonExistingBrandId));

        // Assert
        Assert.Equal($"Brand with id {nonExistingBrandId} does not exist.", exception.Message);
    }
}