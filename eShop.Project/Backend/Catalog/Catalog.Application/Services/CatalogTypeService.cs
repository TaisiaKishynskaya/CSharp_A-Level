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
        _logger.LogInformation($"Getting types with page {page} and size {size}");
        var typesEntities = await _catalogTypeRepository.Get(page, size);

        if (typesEntities == null)
        {
            _logger.LogWarning($"No types found with page {page} and size {size}");
            throw new NotFoundException($"Types not found");
        }

        _logger.LogInformation($"Found {typesEntities.Count()} types with page {page} and size {size}");
        return _mapper.Map<IEnumerable<CatalogType>>(typesEntities);
    }

    public async Task<CatalogType> GetById(int id)
    {
        _logger.LogInformation($"Getting type with id {id}");
        var typeEntity = await _catalogTypeRepository.GetById(id);

        if (typeEntity == null)
        {
            _logger.LogWarning($"Type with id {id} not found");
            throw new NotFoundException($"Type with id = {id} not found");
        }

        _logger.LogInformation($"Found type with id {id}");
        return _mapper.Map<CatalogType>(typeEntity);
    }

    public async Task<int> Add(CatalogType type)
    {
        _logger.LogInformation($"Adding new type with title {type.Title}");
        var typeEntity = _mapper.Map<CatalogTypeEntity>(type);

        var existingTypeEntity = await _catalogTypeRepository.GetByTitle(typeEntity.Title);
        if (existingTypeEntity != null)
        {
            _logger.LogWarning($"Type with title {type.Title} already exists");
            throw new ValidationAsyncException("Title has to be unique");
        }

        typeEntity.CreatedAt = DateTime.UtcNow;

        _logger.LogInformation($"Successfully added type with title {type.Title}");
        return await _catalogTypeRepository.Add(typeEntity);
    }

    public async Task<int> Update(CatalogType type)
    {
        _logger.LogInformation($"Updating type with id {type.Id}");
        var existingTypeEntity = await _catalogTypeRepository.GetById(type.Id);

        if (existingTypeEntity == null)
        {
            _logger.LogWarning($"Type with id {type.Id} not found");
            throw new NotFoundException($"Type with id = {type.Id} not found");
        }

        _mapper.Map(type, existingTypeEntity);
        existingTypeEntity.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation($"Successfully updated type with id {type.Id}");
        return await _catalogTypeRepository.Update(existingTypeEntity);
    }

    public async Task<int> Delete(int id)
    {
        _logger.LogInformation($"Deleting type with id {id}");
        var existingTypeEntity = await _catalogTypeRepository.GetById(id);

        if (existingTypeEntity == null)
        {
            _logger.LogWarning($"Type with id {id} not found");
            throw new NotFoundException($"Type with id = {id} not found");
        }

        _logger.LogInformation($"Successfully deleted type with id {id}");
        return await _catalogTypeRepository.Delete(id);
    }

    public async Task<int> Count()
    {
        _logger.LogInformation("Counting types");
        var count = await _catalogTypeRepository.Count();
        _logger.LogInformation($"Total types count: {count}");
        return count;
    }

    public async Task<CatalogType> GetByTitle(string title)
    {
        _logger.LogInformation($"Getting type with title {title}");
        var typeEntity = await _catalogTypeRepository.GetByTitle(title);
        _logger.LogInformation($"Found type with title {title}");
        return _mapper.Map<CatalogType>(typeEntity);
    }
}