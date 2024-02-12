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
        try
        {
            var startTime = DateTime.UtcNow;
            var brandsEntities = await _catalogBrandRepository.Get(page, size);
            var endTime = DateTime.UtcNow;

            if (brandsEntities == null)
            {
                _logger.LogError($"Error: Brands not found, Stack Trace: {Environment.StackTrace}");
                throw new NotFoundException($"Brands not found");
            }

            _logger.LogInformation($"Operation: Get, Start Time: {startTime}, End Time: {endTime}, Status: Success");
            return _mapper.Map<IEnumerable<CatalogBrand>>(brandsEntities);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<CatalogBrand> GetById(int id)
    {
        try
        {
            var brandEntity = await _catalogBrandRepository.GetById(id);

            if (brandEntity == null)
            {
                _logger.LogError($"Error: Brand with id = {id} not found, Stack Trace: {Environment.StackTrace}");
                throw new NotFoundException($"Brand with id = {id} not found");
            }

            return _mapper.Map<CatalogBrand>(brandEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<int> Add(CatalogBrand brand)
    {
        try
        {
            var brandEntity = _mapper.Map<CatalogBrandEntity>(brand);

            var existingBrand = await _catalogBrandRepository.GetByTitle(brandEntity.Title);
            if (existingBrand != null)
            {
                throw new ValidationAsyncException("Title has to be unique");
            }

            brandEntity.CreatedAt = DateTime.UtcNow;

            return await _catalogBrandRepository.Add(brandEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<int> Update(CatalogBrand brand)
    {
        try
        {
            var existingBrandEntity = await _catalogBrandRepository.GetById(brand.Id);

            if (existingBrandEntity == null)
            {
                _logger.LogError($"Error: Brand with id = {brand.Id} not found, Stack Trace: {Environment.StackTrace}");
                throw new NotFoundException($"Brand with id = {brand.Id} not found");
            }

            _mapper.Map(brand, existingBrandEntity);
            existingBrandEntity.UpdatedAt = DateTime.UtcNow;

            return await _catalogBrandRepository.Update(existingBrandEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<int> Delete(int id)
    {
        try
        {
            var existingBrandEntity = await _catalogBrandRepository.GetById(id);

            if (existingBrandEntity == null)
            {
                _logger.LogError($"Error: Brand with id = {id} not found, Stack Trace: {Environment.StackTrace}");
                throw new NotFoundException($"Brand with id = {id} not found");
            }

            return await _catalogBrandRepository.Delete(id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<int> Count()
    {
        try
        {
            var count = await _catalogBrandRepository.Count();
            return count;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<CatalogBrand> GetByTitle(string title)
    {
        try
        {
            var brandEntity = await _catalogBrandRepository.GetByTitle(title);
            return _mapper.Map<CatalogBrand>(brandEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }
}