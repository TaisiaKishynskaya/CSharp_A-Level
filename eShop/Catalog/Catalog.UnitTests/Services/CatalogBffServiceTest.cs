using Catalog.Host.Data.Entities;
using Catalog.Host.Models.DTOs;
using Catalog.Host.Models.Responses;

namespace Catalog.UnitTests.Services;

public class CatalogBffServiceTest
{
    private readonly ICatalogBffService _catalogBffService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
    private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<BaseDataService<ApplicationDbContext>>> _logger;
    private readonly Mock<IMapper> _mapper;
    
    public CatalogBffServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
        _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogBffService = new CatalogBffService(
            _dbContextWrapper.Object,
            _logger.Object,
            _catalogItemRepository.Object,
            _catalogBrandRepository.Object,
            _catalogTypeRepository.Object,
            _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // Arrange
        var pageIndex = 0;
        var pageSize = 10;

        var catalogItems = new List<CatalogItem>
        {
            new() { Id = 1, Name = "Item1" },
            new() { Id = 2, Name = "Item2" }
        };

        var paginatedItems = new PaginatedItems<CatalogItem>
        {
            TotalCount = catalogItems.Count,
            Data = catalogItems
        };

        var expectedDto = new PaginatedItemsResponse<CatalogItemDto>
        {
            Count = catalogItems.Count,
            Data = catalogItems.Select(i => new CatalogItemDto { Id = i.Id, Name = i.Name }).ToList(),
            PageIndex = pageIndex,
            PageSize = pageSize
        };

        _catalogItemRepository.Setup(r => r.GetItemsByPageAsync(pageIndex, pageSize)).ReturnsAsync(paginatedItems);
        _mapper.Setup(m => m.Map<CatalogItemDto>(It.IsAny<CatalogItem>())).Returns<CatalogItem>(i => new CatalogItemDto { Id = i.Id, Name = i.Name });

        // Act
        var result = await _catalogBffService.GetCatalogItemsAsync(pageIndex, pageSize);

        // Assert
        result.Should().BeEquivalentTo(expectedDto);

        foreach (var item in result.Data)
        {
            var originalItem = catalogItems.First(i => i.Id == item.Id);
            item.Name.Should().Be(originalItem.Name);
        }
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failure()
    {
        // Arrange
        var pageIndex = 0;
        var pageSize = 10;

        _catalogItemRepository.Setup(r => r.GetItemsByPageAsync(pageIndex, pageSize)).ThrowsAsync(new Exception());

        // Act
        var result = await _catalogBffService.GetCatalogItemsAsync(pageIndex, pageSize);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetItemByIdAsync_Success()
    {
        // Arrange
        var id = 1;
        var catalogItem = new CatalogItem { Id = id, Name = "Item1" };

        var expectedDto = new CatalogGetItemDto { Id = id, Name = "Item1" };

        _catalogItemRepository.Setup(r => r.GetItemByIdAsync(id)).ReturnsAsync(catalogItem);
        _mapper.Setup(m => m.Map<CatalogGetItemDto>(It.IsAny<CatalogItem>())).Returns<CatalogItem>(i => new CatalogGetItemDto { Id = i.Id, Name = i.Name });

        // Act
        var result = await _catalogBffService.GetItemByIdAsync(id);

        // Assert
        result.Should().BeEquivalentTo(expectedDto);
        result.Name.Should().Be(catalogItem.Name);
    }

    [Fact]
    public async Task GetItemByIdAsync_Failure()
    {
        // Arrange
        var id = 1;

        _catalogItemRepository.Setup(r => r.GetItemByIdAsync(id)).ThrowsAsync(new Exception());

        // Act
        var result = await _catalogBffService.GetItemByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetItemsByBrandAsync_Success()
    {
        // Arrange
        var brandId = 1;
        var catalogItems = new List<CatalogItem>
        {
            new() { Id = 1, Name = "Item1", CatalogBrandId = brandId },
            new() { Id = 2, Name = "Item2", CatalogBrandId = brandId }
        };

        var expectedDto = new PaginatedItemsResponse<CatalogGetItemDto>
        {
            Count = catalogItems.Count,
            Data = catalogItems.Select(i => new CatalogGetItemDto { Id = i.Id, Name = i.Name }).ToList(),
            PageIndex = 1,
            PageSize = catalogItems.Count
        };

        _catalogItemRepository.Setup(r => r.GetItemsByBrandAsync(brandId)).ReturnsAsync(catalogItems);
        _mapper.Setup(m => m.Map<CatalogGetItemDto>(It.IsAny<CatalogItem>())).Returns<CatalogItem>(i => new CatalogGetItemDto { Id = i.Id, Name = i.Name });

        // Act
        var result = await _catalogBffService.GetItemsByBrandAsync(brandId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedDto);
    }

    [Fact]
    public async Task GetItemsByBrandAsync_Failure()
    {
        // Arrange
        var brandId = 1;

        _catalogItemRepository.Setup(r => r.GetItemsByBrandAsync(brandId)).ReturnsAsync(() => null);

        // Act
        var result = await _catalogBffService.GetItemsByBrandAsync(brandId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetItemsByTypeAsync_Success()
    {
        // Arrange
        var typeId = 1;
        var catalogItems = new List<CatalogItem>
        {
            new() { Id = 1, Name = "Item1", CatalogTypeId = typeId },
            new() { Id = 2, Name = "Item2", CatalogTypeId = typeId }
        };

        var expectedDto = new PaginatedItemsResponse<CatalogGetItemDto>
        {
            Count = catalogItems.Count,
            Data = catalogItems.Select(i => new CatalogGetItemDto { Id = i.Id, Name = i.Name }).ToList(),
            PageIndex = 1,
            PageSize = catalogItems.Count
        };

        _catalogItemRepository.Setup(r => r.GetItemsByTypeAsync(typeId)).ReturnsAsync(catalogItems);
        _mapper.Setup(m => m.Map<CatalogGetItemDto>(It.IsAny<CatalogItem>())).Returns<CatalogItem>(i => new CatalogGetItemDto { Id = i.Id, Name = i.Name });

        // Act
        var result = await _catalogBffService.GetItemsByTypeAsync(typeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedDto);
    }

    [Fact]
    public async Task GetItemsByTypeAsync_Failure()
    {
        // Arrange
        var typeId = 1;

        _catalogItemRepository.Setup(r => r.GetItemsByTypeAsync(typeId)).ReturnsAsync(() => null);

        // Act
        var result = await _catalogBffService.GetItemsByTypeAsync(typeId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetBrandsByPageAsync_Success()
    {
        // Arrange
        var pageIndex = 0;
        var pageSize = 10;

        var catalogBrands = new List<CatalogBrand>
        {
            new() { Id = 1, Brand = "Brand1" },
            new() { Id = 2, Brand = "Brand2" }
        };

        var paginatedItems = new PaginatedItems<CatalogBrand>
        {
            TotalCount = catalogBrands.Count,
            Data = catalogBrands
        };

        var expectedDto = new PaginatedItemsResponse<CatalogBrandDto>
        {
            Count = catalogBrands.Count,
            Data = catalogBrands.Select(i => new CatalogBrandDto { Id = i.Id, Brand = i.Brand }).ToList(),
            PageIndex = pageIndex,
            PageSize = pageSize
        };

        _catalogBrandRepository.Setup(r => r.GetBrandsByPageAsync(pageIndex, pageSize)).ReturnsAsync(paginatedItems);
        _mapper.Setup(m => m.Map<CatalogBrandDto>(It.IsAny<CatalogBrand>())).Returns<CatalogBrand>(i => new CatalogBrandDto { Id = i.Id, Brand = i.Brand });

        // Act
        var result = await _catalogBffService.GetBrandsByPageAsync(pageIndex, pageSize);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedDto);

        foreach (var brand in result.Data)
        {
            var originalBrand = catalogBrands.First(i => i.Id == brand.Id);
            brand.Brand.Should().Be(originalBrand.Brand);
        }
    }

    [Fact]
    public async Task GetBrandsByPageAsync_Failure()
    {
        // Arrange
        var pageIndex = 0;
        var pageSize = 10;

        _catalogBrandRepository.Setup(r => r.GetBrandsByPageAsync(pageIndex, pageSize)).ReturnsAsync(() => null);

        // Act
        var result = await _catalogBffService.GetBrandsByPageAsync(pageIndex, pageSize);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetBrandByIdAsync_Success()
    {
        // Arrange
        var id = 1;
        var catalogBrand = new CatalogBrand { Id = id, Brand = "Brand1" };

        var expectedDto = new CatalogBrandDto { Id = id, Brand = "Brand1" };

        _catalogBrandRepository.Setup(r => r.GetBrandByIdAsync(id)).ReturnsAsync(catalogBrand);
        _mapper.Setup(m => m.Map<CatalogBrandDto>(It.IsAny<CatalogBrand>())).Returns<CatalogBrand>(i => new CatalogBrandDto { Id = i.Id, Brand = i.Brand });

        // Act
        var result = await _catalogBffService.GetBrandByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedDto);
        result.Brand.Should().Be(catalogBrand.Brand);
    }

    [Fact]
    public async Task GetBrandByIdAsync_Failure()
    {
        // Arrange
        var id = 1;

        _catalogBrandRepository.Setup(r => r.GetBrandByIdAsync(id)).ReturnsAsync(() => null);

        // Act
        var result = await _catalogBffService.GetBrandByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetTypesByPageAsync_Success()
    {
        // Arrange
        var pageIndex = 0;
        var pageSize = 10;

        var catalogTypes = new List<CatalogType>
        {
            new() { Id = 1, Type = "Type1" },
            new() { Id = 2, Type = "Type2" }
        };

        var paginatedItems = new PaginatedItems<CatalogType>
        {
            TotalCount = catalogTypes.Count,
            Data = catalogTypes
        };

        var expectedDto = new PaginatedItemsResponse<CatalogTypeDto>
        {
            Count = catalogTypes.Count,
            Data = catalogTypes.Select(i => new CatalogTypeDto { Id = i.Id, Type = i.Type }).ToList(),
            PageIndex = pageIndex,
            PageSize = pageSize
        };

        _catalogTypeRepository.Setup(r => r.GetTypesByPageAsync(pageIndex, pageSize)).ReturnsAsync(paginatedItems);
        _mapper.Setup(m => m.Map<CatalogTypeDto>(It.IsAny<CatalogType>())).Returns<CatalogType>(i => new CatalogTypeDto { Id = i.Id, Type = i.Type });

        // Act
        var result = await _catalogBffService.GetTypesByPageAsync(pageIndex, pageSize);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedDto);

        foreach (var type in result.Data)
        {
            var originalType = catalogTypes.First(i => i.Id == type.Id);
            type.Type.Should().Be(originalType.Type);
        }
    }

    [Fact]
    public async Task GetTypesByPageAsync_Failure()
    {
        // Arrange
        var pageIndex = 0;
        var pageSize = 10;

        _catalogTypeRepository.Setup(r => r.GetTypesByPageAsync(pageIndex, pageSize)).ReturnsAsync(() => null);

        // Act
        var result = await _catalogBffService.GetTypesByPageAsync(pageIndex, pageSize);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetTypeByIdAsync_Success()
    {
        // Arrange
        var id = 1;
        var catalogType = new CatalogType { Id = id, Type = "Type1" };

        var expectedDto = new CatalogTypeDto { Id = id, Type = "Type1" };

        _catalogTypeRepository.Setup(r => r.GetTypeByIdAsync(id)).ReturnsAsync(catalogType);
        _mapper.Setup(m => m.Map<CatalogTypeDto>(It.IsAny<CatalogType>())).Returns<CatalogType>(i => new CatalogTypeDto { Id = i.Id, Type = i.Type });

        // Act
        var result = await _catalogBffService.GetTypeByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedDto);
        result.Type.Should().Be(catalogType.Type);
    }

    [Fact]
    public async Task GetTypeByIdAsync_Failure()
    {
        // Arrange
        var id = 1;

        _catalogTypeRepository.Setup(r => r.GetTypeByIdAsync(id)).ReturnsAsync(() => null);

        // Act
        var result = await _catalogBffService.GetTypeByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }
}