namespace Catalog.Core.Services;

public class CatalogItemService : ICatalogItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<CatalogItemEntity> _catalogItemRepository;

    public CatalogItemService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _catalogItemRepository = _unitOfWork.GetRepository<CatalogItemEntity>();
    }

    public async Task<IEnumerable<CatalogItem>> Get(int page, int size)
    {
        var itemsEntities = await _catalogItemRepository.Get(page, size);

        var items = itemsEntities.Select(item =>
        new CatalogItem
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Price = item.Price,
            PictureFile = item.PictureFile,
            Type = new CatalogType { Id = item.Type.Id, Title = item.Type.Title, CreatedAt = item.CreatedAt, UpdatedAt = item.UpdatedAt},
            Brand = new CatalogBrand { Id = item.Brand.Id, Title = item.Brand.Title, CreatedAt = item.CreatedAt, UpdatedAt = item.UpdatedAt },
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt,
        });

        return items;
    }

    public async Task<CatalogItem> GetById(int id)
    {
        var itemEntity = await _catalogItemRepository.GetById(id);

        var item = new CatalogItem
        {
            Id = itemEntity.Id,
            Title = itemEntity.Title,
            Description = itemEntity.Description,
            Price = itemEntity.Price,
            PictureFile = itemEntity.PictureFile,
            Type = new CatalogType { Title = itemEntity.Type.Title },
            Brand = new CatalogBrand { Title = itemEntity.Brand.Title },
            CreatedAt = itemEntity.CreatedAt,
            UpdatedAt = itemEntity.UpdatedAt,
        };

        return item;
    }

    public async Task<int> Add(CatalogItem item)
    {
        try
        {
            var itemEntity = new CatalogItemEntity
            {
                Title = item.Title,
                Description = item.Description,
                Price = item.Price,
                PictureFile = item.PictureFile,
                Type = new CatalogTypeEntity { Title = item.Type.Title },
                Brand = new CatalogBrandEntity { Title = item.Brand.Title },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            var id = await _catalogItemRepository.Add(itemEntity);
            _unitOfWork.Commit();

            return id;
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();

            throw ex;
        }
    }

    public async Task<CatalogItem> Update(int id, CatalogItem item)
    {
        try
        {
            var itemEntity = new CatalogItemEntity
            {
                Id = id,
                Title = item.Title,
                Description = item.Description,
                Price = item.Price,
                PictureFile = item.PictureFile,
                Type = new CatalogTypeEntity { Title = item.Type.Title },
                Brand = new CatalogBrandEntity { Title = item.Brand.Title },
                UpdatedAt = DateTime.UtcNow
            };

            var updatedEntity = await _catalogItemRepository.Update(itemEntity);
            _unitOfWork.Commit();

            return new CatalogItem
            {
                Id = updatedEntity.Id,
                Title = updatedEntity.Title,
                Description = updatedEntity.Description,
                Price = updatedEntity.Price,
                PictureFile = updatedEntity.PictureFile,
                Type = new CatalogType { Id = updatedEntity.TypeId.Value, Title = updatedEntity.Type.Title },
                Brand = new CatalogBrand { Id = updatedEntity.BrandId.Value, Title = updatedEntity.Brand.Title },
                CreatedAt = updatedEntity.CreatedAt,
                UpdatedAt = updatedEntity.UpdatedAt
            };
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();

            throw ex;
        }
    }

    public async Task<int> Delete(int id)
    {
        try
        {
            var result = await _catalogItemRepository.Delete(id);
            _unitOfWork.Commit();

            return result;
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }

    public async Task<int> Count()
    {
        return await _catalogItemRepository.Count();
    }
}
