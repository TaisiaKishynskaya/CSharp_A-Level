using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.DTOs;
using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Models.Requests.DeleteRequests;
using Catalog.Host.Models.Requests.UpdateRequests;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICatalogItemService _catalogItemService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogItemService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly AddCatalogItemRequest _testItem = new AddCatalogItemRequest
    {
        Name = "TestName",
        Description = "TestDescription",
        Price = 1000M,
        PictureFileName = "1.png",
        CatalogBrandId = 1,
        CatalogTypeId = 1
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogItemService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogItemService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // Arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.AddAsync(
            It.IsAny<AddCatalogItemRequest>())).ReturnsAsync(testResult);

        // Act
        var result = await _catalogItemService.AddAsync(_testItem);

        // Assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // Arrange
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.AddAsync(
            It.IsAny<AddCatalogItemRequest>())).ReturnsAsync(testResult);

        // Act
        var result = await _catalogItemService.AddAsync(_testItem);

        // Assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task GetByPageAsyncHttpGet_Success()
    {
        // Arrange
        var pageIndex = 1;
        var pageSize = 10;

        var catalogItems = new List<CatalogItem>
        {
            new CatalogItem { Id = 1, Name = "Item1", Description = "Description1", Price = 100, PictureFileName = "1.png", CatalogBrand = new CatalogBrand { Brand = "Brand1" }, CatalogType = new CatalogType { Type = "Type1" } },
            new CatalogItem { Id = 2, Name = "Item2", Description = "Description2", Price = 200, PictureFileName = "2.png", CatalogBrand = new CatalogBrand { Brand = "Brand2" }, CatalogType = new CatalogType { Type = "Type2" } }
        };

        var paginatedItems = new PaginatedItems<CatalogItem>
        {
            TotalCount = catalogItems.Count,
            Data = catalogItems
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsyncHttpGet(pageIndex, pageSize)).ReturnsAsync(paginatedItems);

        // Act
        var result = await _catalogItemService.GetByPageAsyncHttpGet(pageIndex, pageSize);

        // Assert
        result.TotalCount.Should().Be(paginatedItems.TotalCount);
        result.Data.Count().Should().Be(paginatedItems.Data.Count());
    }

    [Fact]
    public async Task GetByPageAsyncHttpGet_Failed()
    {
        // Arrange
        var pageIndex = 1;
        var pageSize = 10;

        _catalogItemRepository.Setup(s => s.GetByPageAsyncHttpGet(pageIndex, pageSize)).ReturnsAsync((PaginatedItems<CatalogItem>)null);

        // Act
        var result = await _catalogItemService.GetByPageAsyncHttpGet(pageIndex, pageSize);

        // Assert
        Assert.Null(result);
    }


    [Fact]
    public async Task UpdateAsync_Success()
    {
        // Arrange
        var updateCatalogItem = new UpdateCatalogItemRequest
        {
            Id = 1,
            Name = "UpdatedName",
            Description = "UpdatedDescription",
            Price = 2000M,
            PictureFileName = "2.png",
            CatalogBrandId = 2,
            CatalogTypeId = 2
        };

        var updatedItem = new CatalogItem
        {
            Id = updateCatalogItem.Id,
            Name = updateCatalogItem.Name,
            Description = updateCatalogItem.Description,
            Price = updateCatalogItem.Price,
            PictureFileName = updateCatalogItem.PictureFileName,
            CatalogBrandId = updateCatalogItem.CatalogBrandId,
            CatalogTypeId = updateCatalogItem.CatalogTypeId,
            CatalogBrand = new CatalogBrand { Id = updateCatalogItem.CatalogBrandId, Brand = "TestBrand" },
            CatalogType = new CatalogType { Id = updateCatalogItem.CatalogTypeId, Type = "TestType" }
        };

        var expectedDto = new CatalogGetItemDto
        {
            Id = updatedItem.Id,
            Name = updatedItem.Name,
            Description = updatedItem.Description,
            Price = updatedItem.Price,
            PictureFileName = updatedItem.PictureFileName,
            BrandName = updatedItem.CatalogBrand.Brand,
            TypeName = updatedItem.CatalogType.Type
        };

        _catalogItemRepository.Setup(s => s.UpdateAsync(It.IsAny<UpdateCatalogItemRequest>())).ReturnsAsync(updatedItem);
        _mapper.Setup(m => m.Map<CatalogGetItemDto>(It.IsAny<CatalogItem>())).Returns(expectedDto);

        // Act
        var result = await _catalogItemService.UpdateAsync(updateCatalogItem);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Item);
        Assert.Equal(expectedDto.Id, result.Item.Id);
        Assert.Equal(expectedDto.Name, result.Item.Name);
        Assert.Equal(expectedDto.Description, result.Item.Description);
        Assert.Equal(expectedDto.BrandName, result.Item.BrandName);
        Assert.Equal(expectedDto.TypeName, result.Item.TypeName);
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // Arrange
        var updateCatalogItem = new UpdateCatalogItemRequest
        {
            Id = 1,
            Name = "UpdatedName",
            Description = "UpdatedDescription",
            Price = 2000M,
            PictureFileName = "2.png",
            CatalogBrandId = 2,
            CatalogTypeId = 2
        };

        _catalogItemRepository.Setup(s => s.UpdateAsync(updateCatalogItem)).ReturnsAsync((CatalogItem)null);

        // Act
        var result = await _catalogItemService.UpdateAsync(updateCatalogItem);

        // Assert
        result.Item.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        // Arrange
        var deleteCatalogItem = new DeleteCatalogItemRequest { Id = 1 };

        _catalogItemRepository.Setup(s => s.DeleteAsync(deleteCatalogItem)).Returns(Task.CompletedTask);

        // Act
        await _catalogItemService.DeleteAsync(deleteCatalogItem);

        // Assert
        _catalogItemRepository.Verify(s => s.DeleteAsync(deleteCatalogItem), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Failed()
    {
        // Arrange
        var deleteCatalogItem = new DeleteCatalogItemRequest { Id = 1 };

        _catalogItemRepository.Setup(s => s.DeleteAsync(deleteCatalogItem)).ThrowsAsync(new KeyNotFoundException());

        // Act
        var exception = await Record.ExceptionAsync(() => _catalogItemService.DeleteAsync(deleteCatalogItem));

        // Assert
        Assert.Null(exception);
    }
}