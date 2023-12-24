using Catalog.Host.Data.Entities;
using Catalog.Host.Models.DTOs;
using Catalog.Host.Models.Requests.DeleteRequests;
using Catalog.Host.Models.Requests.UpdateRequests;
using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;

namespace Catalog.UnitTests.Services;

public class CatalogBrandServiceTest
{
    private readonly ICatalogBrandService _catalogBrandService;

    private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogBrandService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly AddCatalogBrandRequest _testBrand = new AddCatalogBrandRequest
    {
        Brand = "Brand1"
    };

    public CatalogBrandServiceTest()
    {
        _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogBrandService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogBrandService = new CatalogBrandService(_dbContextWrapper.Object, _logger.Object, _catalogBrandRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task AddBrandAsync_Success()
    {
        // Arrange
        var testResult = 1;

        _catalogBrandRepository.Setup(s => s.AddAsync(
            It.IsAny<AddCatalogBrandRequest>())).ReturnsAsync(testResult);

        // Act
        var result = await _catalogBrandService.AddAsync(_testBrand);

        // Assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddBrandAsync_Failed()
    {
        // Arrange
        int? testResult = null;

        _catalogBrandRepository.Setup(s => s.AddAsync(
            It.IsAny<AddCatalogBrandRequest>())).ReturnsAsync(testResult);

        // Act
        var result = await _catalogBrandService.AddAsync(_testBrand);

        // Assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateBrandAsync_Success()
    {
        // Arrange
        var updateCatalogBrand = new UpdateCatalogBrandRequest
        {
            Id = 1,
            Brand = "UpdatedBrand"
        };

        var updatedBrand = new CatalogBrand
        {
            Id = updateCatalogBrand.Id,
            Brand = updateCatalogBrand.Brand
        };

        var expectedDto = new CatalogBrandDto
        {
            Id = updatedBrand.Id,
            Brand = updatedBrand.Brand
        };

        _catalogBrandRepository.Setup(s => s.UpdateAsync(It.IsAny<UpdateCatalogBrandRequest>())).ReturnsAsync(updatedBrand);
        _mapper.Setup(m => m.Map<CatalogBrandDto>(It.IsAny<CatalogBrand>())).Returns(expectedDto);

        // Act
        var result = await _catalogBrandService.UpdateAsync(updateCatalogBrand);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Brand);
        Assert.Equal(expectedDto.Id, result.Brand.Id);
        Assert.Equal(expectedDto.Brand, result.Brand.Brand);
    }

    [Fact]
    public async Task UpdateBrandAsync_Failed()
    {
        // Arrange
        var updateCatalogBrand = new UpdateCatalogBrandRequest
        {
            Id = 1,
            Brand = "UpdatedBrand"
        };

        _catalogBrandRepository.Setup(s => s.UpdateAsync(updateCatalogBrand)).ReturnsAsync((CatalogBrand)null);

        // Act
        var result = await _catalogBrandService.UpdateAsync(updateCatalogBrand);

        // Assert
        result.Brand.Should().BeNull();
    }

    [Fact]
    public async Task DeleteBrandAsync_Success()
    {
        // Arrange
        var deleteCatalogBrand = new DeleteCatalogBrandRequest { Id = 1 };

        _catalogBrandRepository.Setup(s => s.DeleteAsync(deleteCatalogBrand)).Returns(Task.CompletedTask);

        // Act
        await _catalogBrandService.DeleteAsync(deleteCatalogBrand);

        // Assert
        _catalogBrandRepository.Verify(s => s.DeleteAsync(deleteCatalogBrand), Times.Once);
    }

    [Fact]
    public async Task DeleteBrandAsync_Failed()
    {
        // Arrange
        var deleteCatalogBrand = new DeleteCatalogBrandRequest { Id = 1 };

        _catalogBrandRepository.Setup(s => s.DeleteAsync(deleteCatalogBrand)).ThrowsAsync(new KeyNotFoundException());

        // Act
        var exception = await Record.ExceptionAsync(() => _catalogBrandService.DeleteAsync(deleteCatalogBrand));

        // Assert
        Assert.Null(exception);
    }


}