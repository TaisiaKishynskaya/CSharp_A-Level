namespace Catalog.Application.Services;

public class CatalogItemService : ICatalogItemService
{
    private readonly ICatalogItemRepository<CatalogItemEntity> _catalogItemRepository;
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly ICatalogTypeService _catalogTypeService;
    private readonly IMapper _mapper;
    private readonly ILogger<CatalogItemService> _logger;

    public CatalogItemService(
        ICatalogItemRepository<CatalogItemEntity> catalogItemRepository,
        ICatalogBrandService catalogBrandService,
        ICatalogTypeService catalogTypeService,
        IMapper mapper,
        ILogger<CatalogItemService> logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _catalogBrandService = catalogBrandService;
        _catalogTypeService = catalogTypeService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CatalogItem>> Get(int page, int size)
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var itemsEntities = await _catalogItemRepository.Get(page, size);
            var endTime = DateTime.UtcNow;

            if (itemsEntities == null)
            {
                _logger.LogError($"Error: Items not found, Stack Trace: {Environment.StackTrace}");
                throw new NotFoundException($"Items not found");
            }

            _logger.LogInformation($"Operation: Get, Start Time: {startTime}, End Time: {endTime}, Status: Success");
            return _mapper.Map<IEnumerable<CatalogItem>>(itemsEntities);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<CatalogItem> GetById(int id)
    {
        try
        {
            var itemEntity = await _catalogItemRepository.GetById(id);

            if (itemEntity == null)
            {
                _logger.LogError($"Error: Item with id = {id} not found, Stack Trace: {Environment.StackTrace}");
                throw new NotFoundException($"Item with id = {id} not found");
            }

            return _mapper.Map<CatalogItem>(itemEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<int> Add(CatalogItem item)
    {
        try
        {
            var existingItemWithPicture = await _catalogItemRepository.GetByPictureFile(item.PictureFile);
            if (existingItemWithPicture != null)
            {
                throw new ValidationException("Picture file must be unique");
            }

            var existingItemWithTitle = await _catalogItemRepository.GetByTitle(item.Title);
            if (existingItemWithTitle != null)
            {
                throw new ValidationException("Title must be unique");
            }

            var existingType = await _catalogTypeService.GetByTitle(item.Type.Title);
            var existingBrand = await _catalogBrandService.GetByTitle(item.Brand.Title);

            if (existingType == null || existingBrand == null)
            {
                throw new NotFoundException("Type or Brand not found");
            }

            var itemEntity = new CatalogItemEntity
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Price = item.Price,
                PictureFile = item.PictureFile,
                Quantity = item.Quantity,
                CreatedAt = DateTime.UtcNow,
                TypeId = existingType.Id,
                BrandId = existingBrand.Id
            };

            return await _catalogItemRepository.Add(itemEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<int> Update(CatalogItem item)
    {
        try
        {
            var existingItemEntity = await _catalogItemRepository.GetById(item.Id);

            if (existingItemEntity == null)
            {
                _logger.LogError($"Error: Item with id = {item.Id} not found, Stack Trace: {Environment.StackTrace}");
                throw new NotFoundException($"Item with id = {item.Id} not found");
            }

            var existingType = await _catalogTypeService.GetByTitle(item.Type.Title);
            var existingBrand = await _catalogBrandService.GetByTitle(item.Brand.Title);

            if (existingType == null || existingBrand == null)
            {
                throw new NotFoundException("Type or Brand not found");
            }

            var existingItemWithPicture = await _catalogItemRepository.GetByPictureFile(item.PictureFile);
            if (existingItemWithPicture != null)
            {
                throw new ValidationException("Picture file must be unique");
            }

            existingItemEntity.Title = item.Title;
            existingItemEntity.Description = item.Description;
            existingItemEntity.Price = item.Price;
            existingItemEntity.PictureFile = item.PictureFile;
            existingItemEntity.Quantity = item.Quantity;
            existingItemEntity.UpdatedAt = DateTime.UtcNow;

            return await _catalogItemRepository.Update(existingItemEntity);
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
            var existingItemEntity = await _catalogItemRepository.GetById(id);

            if (existingItemEntity == null)
            {
                _logger.LogError($"Error: Item with id = {id} not found, Stack Trace: {Environment.StackTrace}");
                throw new NotFoundException($"Item with id = {id} not found");
            }

            return await _catalogItemRepository.Delete(id);
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
            var count = await _catalogItemRepository.Count();
            return count;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}, Stack Trace: {ex.StackTrace}");
            throw;
        }
    }
}