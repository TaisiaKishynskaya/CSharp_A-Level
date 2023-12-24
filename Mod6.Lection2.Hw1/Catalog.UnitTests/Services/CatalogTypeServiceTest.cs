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

public class CatalogTypeServiceTest
{
    private readonly ICatalogTypeService _catalogTypeService;

    private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogTypeService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly AddCatalogTypeRequest _testType = new()
    {
        Type = "Type1"
    };

    public CatalogTypeServiceTest()
    {
        _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogTypeService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogTypeService = new CatalogTypeService(_dbContextWrapper.Object, _logger.Object, _catalogTypeRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task AddTypeAsync_Success()
    {
        // Arrange
        var testResult = 1;

        _catalogTypeRepository.Setup(s => s.AddAsync(
            It.IsAny<AddCatalogTypeRequest>())).ReturnsAsync(testResult);

        // Act
        var result = await _catalogTypeService.AddAsync(_testType);

        // Assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddTypeAsync_Failed()
    {
        // Arrange
        int? testResult = null;

        _catalogTypeRepository.Setup(s => s.AddAsync(
            It.IsAny<AddCatalogTypeRequest>())).ReturnsAsync(testResult);

        // Act
        var result = await _catalogTypeService.AddAsync(_testType);

        // Assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateTypeAsync_Success()
    {
        // Arrange
        var updateCatalogType = new UpdateCatalogTypeRequest
        {
            Id = 1,
            Type = "UpdatedType"
        };

        var updatedType = new CatalogType
        {
            Id = updateCatalogType.Id,
            Type = updateCatalogType.Type
        };

        var expectedDto = new CatalogTypeDto
        {
            Id = updatedType.Id,
            Type = updatedType.Type
        };

        _catalogTypeRepository.Setup(s => s.UpdateAsync(It.IsAny<UpdateCatalogTypeRequest>())).ReturnsAsync(updatedType);
        _mapper.Setup(m => m.Map<CatalogTypeDto>(It.IsAny<CatalogType>())).Returns(expectedDto);

        // Act
        var result = await _catalogTypeService.UpdateAsync(updateCatalogType);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Type);
        Assert.Equal(expectedDto.Id, result.Type.Id);
        Assert.Equal(expectedDto.Type, result.Type.Type);
    }

    [Fact]
    public async Task UpdateTypeAsync_Failed()
    {
        // Arrange
        var updateCatalogType = new UpdateCatalogTypeRequest
        {
            Id = 1,
            Type = "UpdatedType"
        };

        _catalogTypeRepository.Setup(s => s.UpdateAsync(updateCatalogType)).ReturnsAsync((CatalogType)null);

        // Act
        var result = await _catalogTypeService.UpdateAsync(updateCatalogType);

        // Assert
        result.Type.Should().BeNull();
    }

    [Fact]
    public async Task DeleteTypeAsync_Success()
    {
        // Arrange
        var deleteCatalogType = new DeleteCatalogTypeRequest { Id = 1 };

        _catalogTypeRepository.Setup(s => s.DeleteAsync(deleteCatalogType)).Returns(Task.CompletedTask);

        // Act
        await _catalogTypeService.DeleteAsync(deleteCatalogType);

        // Assert
        _catalogTypeRepository.Verify(s => s.DeleteAsync(deleteCatalogType), Times.Once);
    }

    [Fact]
    public async Task DeleteTypeAsync_Failed()
    {
        // Arrange
        var deleteCatalogType = new DeleteCatalogTypeRequest { Id = 1 };

        _catalogTypeRepository.Setup(s => s.DeleteAsync(deleteCatalogType)).ThrowsAsync(new KeyNotFoundException());

        // Act
        var exception = await Record.ExceptionAsync(() => _catalogTypeService.DeleteAsync(deleteCatalogType));

        // Assert
        Assert.Null(exception);
    }


}