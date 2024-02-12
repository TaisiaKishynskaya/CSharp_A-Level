namespace Catalog.Application.Services;

public class CatalogTypeService : ICatalogTypeService
{
    private readonly ICatalogTypeRepository<CatalogTypeEntity> _catalogTypeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CatalogTypeService> _logger;

    public CatalogTypeService(
        ICatalogTypeRepository<CatalogTypeEntity> catalogTypeRepository,
        IMapper mapper,
        ILogger<CatalogTypeService> logger)
    {
        _catalogTypeRepository = catalogTypeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CatalogType>> Get(int page, int size)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var typesEntities = await _catalogTypeRepository.Get(page, size);
            var endTime = DateTime.UtcNow;

            if (typesEntities == null)
            {
                _logger.LogError($"Error: Types not found, Stack Trace: {Environment.StackTrace}");
                throw new NotFoundException($"Types not found");
            }

            _logger.LogInformation($"Operation: Get, Start Time: {startTime}, End Time: {endTime}, Status: Success");
            return _mapper.Map<IEnumerable<CatalogType>>(typesEntities);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<CatalogType> GetById(int id)
    {
        try
        {
            var typeEntity = await _catalogTypeRepository.GetById(id);

            if (typeEntity == null)
            {
                _logger.LogError($"Error: Type with id = {id} not found, Stack Trace: {Environment.StackTrace}");
                throw new NotFoundException($"Type with id = {id} not found");
            }

            return _mapper.Map<CatalogType>(typeEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<int> Add(CatalogType type)
    {
        try
        {
            var typeEntity = _mapper.Map<CatalogTypeEntity>(type);

            var existingTypeEntity = await _catalogTypeRepository.GetByTitle(typeEntity.Title);
            if (existingTypeEntity != null)
            {
                throw new ValidationAsyncException("Title has to be unique");
            }

            typeEntity.CreatedAt = DateTime.UtcNow;

            return await _catalogTypeRepository.Add(typeEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<int> Update(CatalogType type)
    {
        try
        {
            var existingTypeEntity = await _catalogTypeRepository.GetById(type.Id);

            if (existingTypeEntity == null)
            {
                _logger.LogError($"Error: Type with id = {type.Id} not found, Stack Trace: {Environment.StackTrace}");
                throw new NotFoundException($"Type with id = {type.Id} not found");
            }

            _mapper.Map(type, existingTypeEntity);
            existingTypeEntity.UpdatedAt = DateTime.UtcNow;

            return await _catalogTypeRepository.Update(existingTypeEntity);
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
            var existingTypeEntity = await _catalogTypeRepository.GetById(id);

            if (existingTypeEntity == null)
            {
                _logger.LogError($"Error: Type with id = {id} not found, Stack Trace: {Environment.StackTrace}");
                throw new NotFoundException($"Type with id = {id} not found");
            }

            return await _catalogTypeRepository.Delete(id);
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
            var count = await _catalogTypeRepository.Count();
            return count;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<CatalogType> GetByTitle(string title)
    {
        try
        {
            var typeEntity = await _catalogTypeRepository.GetByTitle(title);
            return _mapper.Map<CatalogType>(typeEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }
}