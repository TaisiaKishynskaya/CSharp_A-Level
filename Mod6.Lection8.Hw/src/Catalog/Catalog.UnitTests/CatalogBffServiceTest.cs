namespace Catalog.UnitTests;

public class CatalogBffServiceTest
{
    private readonly ICatalogBffService _catalogBffService;
    private readonly Mock<ICatalogBffRepository> _catalogBffRepository;
    private readonly Mock<ILogger<CatalogBffService>> _logger;

    private readonly List<CatalogBrand> _catalogBrands = new List<CatalogBrand>
    {
        new CatalogBrand { Id = 1, Brand = "Brand1" },
        new CatalogBrand { Id = 2, Brand = "Brand2" }
    };

    private readonly List<CatalogType> _catalogTypes = new List<CatalogType>
    {
        new CatalogType { Id = 1, Type = "Type1" },
        new CatalogType { Id = 2, Type = "Type2" }
    };

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

    public CatalogBffServiceTest()
    {
        _catalogBffRepository = new Mock<ICatalogBffRepository>();
        _logger = new Mock<ILogger<CatalogBffService>>();

        _catalogBffService = new CatalogBffService(_catalogBffRepository.Object, _logger.Object);
    }

    [Fact]
    public async Task GetTypes_ReturnsPaginatedItems_WhenSuccessful()
    {
        // Arrange
        _catalogBffRepository.Setup(repo => repo.GetTypes()).ReturnsAsync(_catalogTypes);

        // Act
        var result = await _catalogBffService.GetTypes(1, 1);

        // Assert
        Assert.Single(result.Data);
        Assert.Equal(1, result.PageIndex);
        Assert.Equal(1, result.PageSize);
        Assert.Equal(_catalogTypes.Count, result.count);
    }

    [Fact]
    public async Task GetTypes_ReturnsEmpty_WhenNoData()
    {
        // Arrange
        _catalogBffRepository.Setup(repo => repo.GetTypes()).ReturnsAsync(new List<CatalogType>());

        // Act
        var result = await _catalogBffService.GetTypes(1, 1);

        // Assert
        Assert.Empty(result.Data);
        Assert.Equal(1, result.PageIndex);
        Assert.Equal(1, result.PageSize);
        Assert.Equal(0, result.count);
    }

    [Fact]
    public async Task GetBrands_ReturnsPaginatedItems_WhenSuccessful()
    {
        // Arrange
        _catalogBffRepository.Setup(repo => repo.GetBrands()).ReturnsAsync(_catalogBrands);

        // Act
        var result = await _catalogBffService.GetBrands(1, 1);

        // Assert
        Assert.Single(result.Data);
        Assert.Equal(1, result.PageIndex);
        Assert.Equal(1, result.PageSize);
        Assert.Equal(_catalogBrands.Count, result.count);
    }

    [Fact]
    public async Task GetBrands_ReturnsEmpty_WhenNoData()
    {
        // Arrange
        _catalogBffRepository.Setup(repo => repo.GetBrands()).ReturnsAsync(new List<CatalogBrand>());

        // Act
        var result = await _catalogBffService.GetBrands(1, 1);

        // Assert
        Assert.Empty(result.Data);
        Assert.Equal(1, result.PageIndex);
        Assert.Equal(1, result.PageSize);
        Assert.Equal(0, result.count);
    }

    [Fact]
    public async Task GetItems_ReturnsFilteredItems_WhenSuccessful()
    {
        // Arrange
        _catalogBffRepository.Setup(repo => repo.GetItems()).ReturnsAsync(_catalogItems);
        var request = new PaginatedItemsRequest
        {
            PageIndex = 1,
            PageSize = 1, 
            Types = new List<string> { "Type1" },
            Brands = new List<string> { "Brand1" } 
        };

        // Act
        var result = await _catalogBffService.GetItems(request);

        // Assert
        Assert.Single(result.Data);
        Assert.Equal(1, result.PageIndex);
        Assert.Equal(1, result.PageSize);
        Assert.Equal(1, result.count);
    }

    [Fact]
    public async Task GetItems_ReturnsEmpty_WhenNoMatchingData()
    {
        // Arrange
        _catalogBffRepository.Setup(repo => repo.GetItems()).ReturnsAsync(_catalogItems);
        var request = new PaginatedItemsRequest 
        { 
            PageIndex = 1, 
            PageSize = 1, 
            Types = new List<string> { "NonExistingType" }, 
            Brands = new List<string> { "NonExistingBrand" } 
        };

        // Act
        var result = await _catalogBffService.GetItems(request);

        // Assert
        Assert.Empty(result.Data);
        Assert.Equal(1, result.PageIndex);
        Assert.Equal(1, result.PageSize);
        Assert.Equal(0, result.count);
    }
}
