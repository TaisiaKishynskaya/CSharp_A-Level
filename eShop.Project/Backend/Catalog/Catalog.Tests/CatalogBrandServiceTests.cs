using Moq;

namespace Catalog.Tests;

// Этот код представляет собой юнит-тесты для класса CatalogBrandService. 

public class CatalogBrandServiceTests
{
    // Объекты-заглушки (Mock) используются для замены зависимостей, таких как репозиторий (ICatalogBrandRepository) и маппер (IMapper),
    // чтобы изолировать тестируемый класс от реальных зависимостей.
    private readonly Mock<ICatalogBrandRepository<CatalogBrandEntity>> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<CatalogBrandService>> _mockLogger;

    private readonly CatalogBrandService _service; // Создается экземпляр тестируемого класса, который будет использоваться в тестах.

    private readonly List<CatalogBrandEntity> _catalogBrandEntities; // Это список сущностей CatalogBrandEntity, который будет использоваться для настройки поведения заглушек.

    public CatalogBrandServiceTests()
    {
        _mockRepo = new();
        _mockMapper = new();
        _mockLogger = new();

        _service = new CatalogBrandService(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);

        _catalogBrandEntities = new()
        {
            new CatalogBrandEntity { Id = 1, Title = "Brand1", CreatedAt = DateTime.UtcNow, UpdatedAt = null},
            new CatalogBrandEntity { Id = 2, Title = "Brand2", CreatedAt = DateTime.UtcNow, UpdatedAt = null}
        };
    }

    // Этот тест проверяет, что метод Get возвращает правильно отображенные бренды из репозитория.
    [Fact] // Это атрибуты методов, которые сообщают тестовому фреймворку, что методы под ними являются фактическими тестами.
    public async Task Get_ReturnsMappedCatalogBrands_WhenRepositoryReturnsEntities()
    {
        //Arrange - Этот блок подготавливает тестируемое окружение, настраивает поведение заглушек и создает необходимые данные.
        _mockRepo.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_catalogBrandEntities);

        var expectedCatalogBrands = new List<CatalogBrand>
        {
            new() { Id = 1, Title = "Brand1", CreatedAt = DateTime.UtcNow, UpdatedAt = null},
            new() { Id = 2, Title = "Brand2", CreatedAt = DateTime.UtcNow, UpdatedAt = null}
        };

        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<CatalogBrand>>(_catalogBrandEntities)).Returns(expectedCatalogBrands);

        //Act - Этот блок вызывает метод, который будет тестироваться.
        var result = await _service.Get(1, 10);

        //Assert - Этот блок проверяет ожидаемый результат метода на соответствие фактическому результату.
        Assert.Equal(expectedCatalogBrands, result);
        _mockRepo.Verify(repo => repo.Get(1, 10), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<IEnumerable<CatalogBrand>>(_catalogBrandEntities), Times.Once);
    }

    // Этот тест проверяет, что метод Get выбрасывает исключение NotFoundException, когда репозиторий возвращает null.
    [Fact]
    public async Task Get_ThrowsNotFoundException_WhenRepositoryReturnsNull()
    {
        //Arrange
        _mockRepo.Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((List<CatalogBrandEntity>)null);

        //Act
        await Assert.ThrowsAsync<NotFoundException>(() => _service.Get(1, 10));

        //Assert
        _mockRepo.Verify(repo => repo.Get(1, 10), Times.Once);
    }

    // Этот тест проверяет, что метод GetById возвращает правильно отображенный бренд из репозитория.
    [Fact]
    public async Task GetById_ReturnsMappedCatalogBrands_WhenRepositoryReturnsEntities()
    {
        //Arrange
        var catalogBrandEntity = _catalogBrandEntities[0];
        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(catalogBrandEntity);

        var expectedCatalogBrand = new CatalogBrand { Id = 1, Title = "Brand1", CreatedAt = DateTime.UtcNow, UpdatedAt = null };
        _mockMapper.Setup(mapper => mapper.Map<CatalogBrand>(catalogBrandEntity)).Returns(expectedCatalogBrand);

        //Act
        var result = await _service.GetById(1);

        //Assert
        Assert.Equal(expectedCatalogBrand, result);
        _mockRepo.Verify(repo => repo.GetById(1), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<CatalogBrand>(catalogBrandEntity), Times.Once);
    }

    // Этот тест проверяет, что метод GetById выбрасывает исключение NotFoundException, когда репозиторий возвращает null.
    [Fact]
    public async Task GetById_ThrowsNotFoundException_WhenRepositoryReturnsNull()
    {
        //Arrange
        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((CatalogBrandEntity)null);

        //Act
        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetById(1));

        //Assert
        _mockRepo.Verify(repo => repo.GetById(1), Times.Once);
    }

    // Этот тест проверяет, что метод Add возвращает правильный идентификатор, когда бренд уникален.
    [Fact]
    public async Task Add_ReturnsId_WhenBrandIsUnique()
    {
        //Arrange
        var catalogBrand = new CatalogBrand { Id = 3, Title = "Brand3", CreatedAt = DateTime.UtcNow, UpdatedAt = null };
        var catalogBrandEntity = new CatalogBrandEntity { Id = 3, Title = "Brand3", CreatedAt = DateTime.UtcNow, UpdatedAt = null };

        _mockRepo.Setup(repo => repo.GetByTitle(It.IsAny<string>())).ReturnsAsync((CatalogBrandEntity)null);
        _mockRepo.Setup(repo => repo.Add(It.IsAny<CatalogBrandEntity>())).ReturnsAsync(3);
        _mockMapper.Setup(mapper => mapper.Map<CatalogBrandEntity>(catalogBrand)).Returns(catalogBrandEntity);

        //Act
        var result = await _service.Add(catalogBrand);

        //Assert
        Assert.Equal(3, result);
        _mockRepo.Verify(repo => repo.GetByTitle("Brand3"), Times.Once);
        _mockRepo.Verify(repo => repo.Add(catalogBrandEntity), Times.Once);
    }

    // Этот тест проверяет, что метод Add выбрасывает исключение ValidationAsyncException, когда бренд не уникален.
    [Fact]
    public async Task Add_ThrowsValidationAsyncException_WhenBrandIsNotUnique()
    {
        //Arrange
        var catalogBrand = new CatalogBrand { Id = 1, Title = "Brand1", CreatedAt = DateTime.UtcNow, UpdatedAt = null };
        var catalogBrandEntity = new CatalogBrandEntity { Id = 1, Title = "Brand1", CreatedAt = DateTime.UtcNow, UpdatedAt = null };

        _mockRepo.Setup(repo => repo.GetByTitle(It.IsAny<string>())).ReturnsAsync(catalogBrandEntity);
        _mockMapper.Setup(mapper => mapper.Map<CatalogBrandEntity>(catalogBrand)).Returns(catalogBrandEntity);

        //Act
        await Assert.ThrowsAsync<ValidationAsyncException>(() => _service.Add(catalogBrand));

        //Assert
        _mockRepo.Verify(repo => repo.GetByTitle("Brand1"), Times.Once);
    }

    // Этот тест проверяет, что метод Update возвращает правильный идентификатор, когда бренд существует.
    [Fact]
    public async Task Update_ReturnsId_WhenBrandExists()
    {
        //Arrange
        var catalogBrand = new CatalogBrand { Id = 1, Title = "Brand1", CreatedAt = DateTime.UtcNow, UpdatedAt = null };
        var catalogBrandEntity = new CatalogBrandEntity { Id = 1, Title = "Brand1", CreatedAt = DateTime.UtcNow, UpdatedAt = null };

        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(catalogBrandEntity);
        _mockRepo.Setup(repo => repo.Update(It.IsAny<CatalogBrandEntity>())).ReturnsAsync(1);
        _mockMapper.Setup(mapper => mapper.Map(catalogBrand, catalogBrandEntity)).Returns(catalogBrandEntity);

        //Act
        var result = await _service.Update(catalogBrand);

        //Assert
        Assert.Equal(1, result);
        _mockRepo.Verify(repo => repo.GetById(1), Times.Once);
        _mockRepo.Verify(repo => repo.Update(catalogBrandEntity), Times.Once);
    }

    // Этот тест проверяет, что метод Update выбрасывает исключение NotFoundException, когда бренд не существует.
    [Fact]
    public async Task Update_ThrowsNotFoundException_WhenBrandDoesNotExist()
    {
        //Arrange
        var catalogBrand = new CatalogBrand { Id = 1, Title = "Brand1", CreatedAt = DateTime.UtcNow, UpdatedAt = null };

        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync((CatalogBrandEntity)null);

        //Act
        await Assert.ThrowsAsync<NotFoundException>(() => _service.Update(catalogBrand));

        //Assert
        _mockRepo.Verify(repo => repo.GetById(1), Times.Once);
    }

    // Этот тест проверяет, что метод Delete возвращает 0, когда бренд существует.
    [Fact]
    public async Task Delete_ReturnsZero_WhenBrandExists()
    {
        // Arrange
        var id = 1;
        _mockRepo.Setup(repo => repo.GetById(id)).ReturnsAsync(new CatalogBrandEntity { Id = id });
        _mockRepo.Setup(repo => repo.Delete(id)).ReturnsAsync(0);

        // Act
        var result = await _service.Delete(id);

        // Assert
        Assert.Equal(0, result);
        _mockRepo.Verify(repo => repo.Delete(id), Times.Once);
    }

    // Этот тест проверяет, что метод Delete выбрасывает исключение NotFoundException, когда бренд не существует.
    [Fact]
    public async Task Delete_ThrowsNotFoundException_WhenBrandDoesNotExist()
    {
        // Arrange
        var id = 1;
        _mockRepo.Setup(repo => repo.GetById(id)).ReturnsAsync((CatalogBrandEntity)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.Delete(id));
        _mockRepo.Verify(repo => repo.GetById(id), Times.Once);
    }

    // Этот тест проверяет, что метод Count возвращает правильное количество брендов.
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

    // Этот тест проверяет, что метод Count выбрасывает исключение, когда репозиторий выбрасывает исключение.
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
    // Этот тест проверяет, что метод GetByTitle возвращает правильный бренд по его названию.
    [Fact]
    public async Task GetByTitle_ReturnsCorrectBrand()
    {
        // Arrange
        var title = "Brand1";
        var catalogBrandEntity = new CatalogBrandEntity { Id = 1, Title = title };
        var catalogBrand = new CatalogBrand { Id = 1, Title = title };

        _mockRepo.Setup(repo => repo.GetByTitle(title)).ReturnsAsync(catalogBrandEntity);
        _mockMapper.Setup(mapper => mapper.Map<CatalogBrand>(catalogBrandEntity)).Returns(catalogBrand);

        // Act
        var result = await _service.GetByTitle(title);

        // Assert
        Assert.Equal(catalogBrand, result);
        _mockRepo.Verify(repo => repo.GetByTitle(title), Times.Once);
    }

    // Этот тест проверяет, что метод GetByTitle возвращает null, когда название бренда не существует.
    [Fact]
    public async Task GetByTitle_ReturnsNull_WhenTitleDoesNotExist()
    {
        // Arrange
        var title = "NonExistentTitle";
        _mockRepo.Setup(repo => repo.GetByTitle(title)).ReturnsAsync((CatalogBrandEntity)null);

        // Act
        var result = await _service.GetByTitle(title);

        // Assert
        Assert.Null(result);
        _mockRepo.Verify(repo => repo.GetByTitle(title), Times.Once);
    }
}
