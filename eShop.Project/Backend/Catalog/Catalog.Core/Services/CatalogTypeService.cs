namespace Catalog.Core.Services;

public class CatalogTypeService : ICatalogTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<CatalogTypeEntity> _catalogTypeRepository;

    public CatalogTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _catalogTypeRepository = _unitOfWork.GetRepository<CatalogTypeEntity>();
    }

    public async Task<IEnumerable<CatalogType>> Get(int page, int size)
    {
        var typesEntities = await _catalogTypeRepository.Get(page, size);

        var types = typesEntities.Select(type =>
        new CatalogType
        {
            Id = type.Id,
            Title = type.Title,
            CreatedAt = type.CreatedAt,
            UpdatedAt = type.UpdatedAt
        });

        return types;
    }

    public async Task<CatalogType> GetById(int id)
    {
        var typeEntity = await _catalogTypeRepository.GetById(id);

        if (typeEntity == null)
        {
            return null;
        }

        var type = new CatalogType
        {
            Id = typeEntity.Id,
            Title = typeEntity.Title,
            CreatedAt = typeEntity.CreatedAt,
            UpdatedAt = typeEntity.UpdatedAt
        };

        return type;
    }

    public async Task<int> Add(CatalogType type)
    {
        try
        {
            var typeEntity = new CatalogTypeEntity
            {
                Id = type.Id,
                Title = type.Title,
                CreatedAt = type.CreatedAt,
                UpdatedAt = type.UpdatedAt,
            };

            await _catalogTypeRepository.Add(typeEntity);
            _unitOfWork.Commit();

            return typeEntity.Id;
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }

    public async Task<CatalogType> Update(int id, CatalogTypeRequest request)
    {
        try
        {
            var typeEntity = new CatalogTypeEntity
            {
                Id = id,
                Title = request.Title,
                UpdatedAt = DateTime.UtcNow
            };

            typeEntity = await _catalogTypeRepository.Update(typeEntity);
            _unitOfWork.Commit();

            var type = new CatalogType
            {
                Id = typeEntity.Id,
                Title = typeEntity.Title,
                CreatedAt = typeEntity.CreatedAt,
                UpdatedAt = typeEntity.UpdatedAt,
            };

            return type;
        }
        catch
        {
            _unitOfWork.Rollback();
            throw new Exception("Update item error");
        }
    }

    public async Task<int> Delete(int id)
    {
        try
        {
            var result = await _catalogTypeRepository.Delete(id);
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
        return await _catalogTypeRepository.Count();
    }
}
