namespace Catalog.API.Services;

public class CatalogItemService : ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly ILogger<CatalogItemService> _logger;
    private readonly IWebHostEnvironment _env;

    public CatalogItemService(
        ICatalogItemRepository catalogItemRepository,
        ILogger<CatalogItemService> logger,
        IWebHostEnvironment env)
    {
        _catalogItemRepository = catalogItemRepository;
        _logger = logger;
        _env = env;
    }

    public async Task<PaginatedItems<CatalogItemDTO>> Get(int pageIndex, int pageSize)
    {
        var items = await _catalogItemRepository.Get();

        var dtoItems = items
            .Select(item => new CatalogItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                PictureUri = item.PictureUri,
                TypeName = item.CatalogType.Type,
                BrandName = item.CatalogBrand.Brand,
                AvailableStock = item.AvailableStock
            })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var response = new PaginatedItems<CatalogItemDTO>(pageIndex, pageSize, items.Count(), dtoItems);

        return response;
    }

    public async Task<CatalogItemDTO> GetById(int id)
    {
        var item = await _catalogItemRepository.GetById(id);
        if (item == null)
        {
            throw new Exception($"Item with id {id} does not exist.");
        }

        return new CatalogItemDTO
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price,
            PictureUri = item.PictureUri,
            TypeName = item.CatalogType.Type,
            BrandName = item.CatalogBrand.Brand,
            AvailableStock = item.AvailableStock
        };
    }

    public async Task<string> GetPicturePathById(int id)
    {
        var pictureUri = await _catalogItemRepository.GetPictureUriById(id);
        if (pictureUri == null)
        {
            throw new Exception($"Item with id {id} does not exist.");
        }

        var imagePath = Path.Combine(_env.ContentRootPath, "Pics", pictureUri);
        if (!File.Exists(imagePath))
        {
            throw new Exception($"Image for item with id {id} does not exist.");
        }

        return imagePath;
    }

    public async Task<string> GetPictureUriById(int id)
    {
        var pictureUri = await _catalogItemRepository.GetPictureUriById(id);
        if (pictureUri == null)
        {
            throw new Exception($"Item with id {id} does not exist.");
        }

        return Path.Combine("Pics", pictureUri);
    }

    public async Task<int> Add(CatalogItemRequest catalogItemRequest)
    {
        var catalogItem = new CatalogItem
        {
            Name = catalogItemRequest.Name,
            Description = catalogItemRequest.Description,
            Price = catalogItemRequest.Price,
            PictureUri = catalogItemRequest.PictureUri,
            CatalogTypeId = catalogItemRequest.TypeId,
            CatalogBrandId = catalogItemRequest.BrandId,
            AvailableStock = catalogItemRequest.AvailableStock
        };

        return await _catalogItemRepository.Add(catalogItem);
    }

    public async Task<CatalogItemDTO> Update(CatalogItem catalogItem)
    {
        var existingCatalogItem = await _catalogItemRepository.GetById(catalogItem.Id);
        if (existingCatalogItem == null)
        {
            throw new ArgumentException($"Item with id {catalogItem.Id} does not exist.");
        }

        existingCatalogItem.Name = catalogItem.Name;
        existingCatalogItem.Description = catalogItem.Description;
        existingCatalogItem.Price = catalogItem.Price;
        existingCatalogItem.PictureUri = catalogItem.PictureUri;
        existingCatalogItem.CatalogTypeId = catalogItem.CatalogTypeId;
        existingCatalogItem.CatalogBrandId = catalogItem.CatalogBrandId;
        existingCatalogItem.AvailableStock = catalogItem.AvailableStock;

        await _catalogItemRepository.Update(existingCatalogItem);

        return new CatalogItemDTO
        {
            Id = existingCatalogItem.Id,
            Name = existingCatalogItem.Name,
            Description = existingCatalogItem.Description,
            Price = existingCatalogItem.Price,
            PictureUri = existingCatalogItem.PictureUri,
            TypeName = existingCatalogItem.CatalogType.Type,
            BrandName = existingCatalogItem.CatalogBrand.Brand,
            AvailableStock = existingCatalogItem.AvailableStock
        };
    }

    public async Task<int> Delete(int id)
    {
        var existingCatalogItem = await _catalogItemRepository.GetById(id);
        if (existingCatalogItem == null)
        {
            throw new ArgumentException($"Item with id {id} does not exist.");
        }

        await _catalogItemRepository.Delete(id);
        return id;
    }

}

