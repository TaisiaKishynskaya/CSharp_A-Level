using System.ComponentModel.DataAnnotations;

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
        _logger.LogInformation($"Getting items with page {page} and size {size}");
        var itemsEntities = await _catalogItemRepository.Get(page, size);

        if (itemsEntities == null)
        {
            _logger.LogWarning($"No items found with page {page} and size {size}");
            throw new NotFoundException($"Items not found");
        }

        _logger.LogInformation($"Found {itemsEntities.Count()} items with page {page} and size {size}");
        return _mapper.Map<IEnumerable<CatalogItem>>(itemsEntities);
    }

    public async Task<CatalogItem> GetById(int id)
    {
        _logger.LogInformation($"Getting item with id {id}");
        var itemEntity = await _catalogItemRepository.GetById(id);

        if (itemEntity == null)
        {
            _logger.LogWarning($"Item with id {id} not found");
            throw new NotFoundException($"Item with id = {id} not found");
        }

        _logger.LogInformation($"Found item with id {id}");
        return _mapper.Map<CatalogItem>(itemEntity);
    }

    public async Task<int> Add(CatalogItem item)
    {
        _logger.LogInformation($"Adding new item with title {item.Title}");
        var existingItemWithPicture = await _catalogItemRepository.GetByPictureFile(item.PictureFile);
        if (existingItemWithPicture != null)
        {
            _logger.LogWarning($"Item with picture file {item.PictureFile} already exists");
            throw new ValidationException("Picture file must be unique");
        }

        var existingItemWithTitle = await _catalogItemRepository.GetByTitle(item.Title);
        if (existingItemWithTitle != null)
        {
            _logger.LogWarning($"Item with title {item.Title} already exists");
            throw new ValidationException("Title must be unique");
        }

        var existingType = await _catalogTypeService.GetByTitle(item.Type.Title);
        var existingBrand = await _catalogBrandService.GetByTitle(item.Brand.Title);

        if (existingType == null || existingBrand == null)
        {
            _logger.LogWarning($"Type or Brand not found");
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

        _logger.LogInformation($"Successfully added item with title {item.Title}");
        return await _catalogItemRepository.Add(itemEntity);
    }

    public async Task<int> Update(CatalogItem item)
    {
        _logger.LogInformation($"Updating item with id {item.Id}");
        var existingItemEntity = await _catalogItemRepository.GetById(item.Id);

        if (existingItemEntity == null)
        {
            _logger.LogWarning($"Item with id {item.Id} not found");
            throw new NotFoundException($"Item with id = {item.Id} not found");
        }

        var existingType = await _catalogTypeService.GetByTitle(item.Type.Title);
        var existingBrand = await _catalogBrandService.GetByTitle(item.Brand.Title);

        if (existingType == null || existingBrand == null)
        {
            _logger.LogWarning("Type or Brand not found");
            throw new NotFoundException("Type or Brand not found");
        }

        var existingItemWithPicture = await _catalogItemRepository.GetByPictureFile(item.PictureFile);
        if (existingItemWithPicture != null)
        {
            _logger.LogWarning($"Item with picture file {item.PictureFile} already exists");
            throw new ValidationException("Picture file must be unique");
        }

        existingItemEntity.Title = item.Title;
        existingItemEntity.Description = item.Description;
        existingItemEntity.Price = item.Price;
        existingItemEntity.PictureFile = item.PictureFile;
        existingItemEntity.Quantity = item.Quantity;
        existingItemEntity.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation($"Successfully updated item with id {item.Id}");
        return await _catalogItemRepository.Update(existingItemEntity);
    }

    public async Task<int> Delete(int id)
    {
        _logger.LogInformation($"Deleting item with id {id}");
        var existingItemEntity = await _catalogItemRepository.GetById(id);

        if (existingItemEntity == null)
        {
            _logger.LogWarning($"Item with id {id} not found");
            throw new NotFoundException($"Item with id = {id} not found");
        }

        _logger.LogInformation($"Successfully deleted item with id {id}");
        return await _catalogItemRepository.Delete(id);
    }

    public async Task<int> Count()
    {
        _logger.LogInformation("Counting items");
        var count = await _catalogItemRepository.Count();
        _logger.LogInformation($"Total items count: {count}");
        return count;
    }
}