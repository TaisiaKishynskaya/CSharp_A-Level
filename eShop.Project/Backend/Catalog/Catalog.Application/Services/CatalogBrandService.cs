namespace Catalog.Application.Services;

public class CatalogBrandService : ICatalogBrandService
{
    private readonly ICatalogBrandRepository<CatalogBrandEntity> _catalogBrandRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CatalogBrandService> _logger;

    public CatalogBrandService(
        ICatalogBrandRepository<CatalogBrandEntity> catalogBrandRepository,
        IMapper mapper,
        ILogger<CatalogBrandService> logger)
    {
        _catalogBrandRepository = catalogBrandRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CatalogBrand>> Get(int page, int size)
    {
        _logger.LogInformation($"Getting brands with page {page} and size {size}");
        var brandsEntities = await _catalogBrandRepository.Get(page, size);

        if (brandsEntities == null)
        {
            _logger.LogWarning($"No brands found with page {page} and size {size}");
            throw new NotFoundException($"Brands not found");
        }

        _logger.LogInformation($"Found {brandsEntities.Count()} brands with page {page} and size {size}");
        return _mapper.Map<IEnumerable<CatalogBrand>>(brandsEntities);
    }

    public async Task<CatalogBrand> GetById(int id)
    {
        _logger.LogInformation($"Getting brand with id {id}");
        var brandEntity = await _catalogBrandRepository.GetById(id);

        if (brandEntity == null)
        {
            _logger.LogWarning($"Brand with id {id} not found");
            throw new NotFoundException($"Brand with id = {id} not found");
        }

        _logger.LogInformation($"Found brand with id {id}");
        return _mapper.Map<CatalogBrand>(brandEntity);
    }

    public async Task<int> Add(CatalogBrand brand)
    {
        _logger.LogInformation($"Adding new brand with title {brand.Title}");
        var brandEntity = _mapper.Map<CatalogBrandEntity>(brand);

        var existingBrand = await _catalogBrandRepository.GetByTitle(brandEntity.Title);
        if (existingBrand != null)
        {
            _logger.LogWarning($"Brand with title {brand.Title} already exists");
            throw new ValidationAsyncException("Title has to be unique");
        }

        brandEntity.CreatedAt = DateTime.UtcNow;

        _logger.LogInformation($"Successfully added brand with title {brand.Title}");
        return await _catalogBrandRepository.Add(brandEntity);
    }

    public async Task<int> Update(CatalogBrand brand)
    {
        _logger.LogInformation($"Updating brand with id {brand.Id}");
        var existingBrandEntity = await _catalogBrandRepository.GetById(brand.Id);

        if (existingBrandEntity == null)
        {
            _logger.LogWarning($"Brand with id {brand.Id} not found");
            throw new NotFoundException($"Brand with id = {brand.Id} not found");
        }

        _mapper.Map(brand, existingBrandEntity);
        existingBrandEntity.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation($"Successfully updated brand with id {brand.Id}");
        return await _catalogBrandRepository.Update(existingBrandEntity);
    }

    public async Task<int> Delete(int id)
    {
        _logger.LogInformation($"Deleting brand with id {id}");
        var existingBrandEntity = await _catalogBrandRepository.GetById(id);

        if (existingBrandEntity == null)
        {
            _logger.LogWarning($"Brand with id {id} not found");
            throw new NotFoundException($"Brand with id = {id} not found");
        }

        _logger.LogInformation($"Successfully deleted brand with id {id}");
        return await _catalogBrandRepository.Delete(id);
    }

    public async Task<int> Count()
    {
        _logger.LogInformation("Counting brands");
        var count = await _catalogBrandRepository.Count();
        _logger.LogInformation($"Total brands count: {count}");
        return count;
    }

    public async Task<CatalogBrand> GetByTitle(string title)
    {
        _logger.LogInformation($"Getting brand with title {title}");
        var brandEntity = await _catalogBrandRepository.GetByTitle(title);
        _logger.LogInformation($"Found brand with title {title}");
        return _mapper.Map<CatalogBrand>(brandEntity);
    }
}