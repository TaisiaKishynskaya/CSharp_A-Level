namespace Catalog.Core.Services;

public class CatalogBrandService : ICatalogBrandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<CatalogBrandEntity> _catalogBrandRepository;

    public CatalogBrandService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _catalogBrandRepository = _unitOfWork.GetRepository<CatalogBrandEntity>();
    }

    public async Task<IEnumerable<CatalogBrand>> Get(int page, int size)
    {
        var brandsEntities = await _catalogBrandRepository.Get(page, size);

        var brands = brandsEntities.Select(brand =>
        new CatalogBrand
        {
            Id = brand.Id,
            Title = brand.Title,
            CreatedAt = brand.CreatedAt,
            UpdatedAt = brand.UpdatedAt
        });
        
        return brands;
    }

    public async Task<CatalogBrand> GetById(int id)
    {
        var brandEntity = await _catalogBrandRepository.GetById(id);

        if (brandEntity == null)
        {
            return null;
        }

        var brand = new CatalogBrand
        {
            Id = brandEntity.Id,
            Title = brandEntity.Title,
            CreatedAt = brandEntity.CreatedAt,
            UpdatedAt = brandEntity.UpdatedAt,
        };

        return brand;
    }

    public async Task<int> Add(CatalogBrand brand)
    {
        try
        {

            var brandEntity = new CatalogBrandEntity
            {
                Id = brand.Id,
                Title = brand.Title,
                CreatedAt = brand.CreatedAt,
                UpdatedAt = brand.UpdatedAt,
            };

            await _catalogBrandRepository.Add(brandEntity);
            _unitOfWork.Commit();

            return brandEntity.Id;
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }

    public async Task<CatalogBrand> Update(int id, CatalogBrandRequest request)
    {
        try
        {
            var brandEntity = new CatalogBrandEntity
            {
                Id = id,
                Title = request.Title,
                UpdatedAt = DateTime.UtcNow
            };

            brandEntity = await _catalogBrandRepository.Update(brandEntity);
            _unitOfWork.Commit();

            var brand = new CatalogBrand
            {
                Id = brandEntity.Id,
                Title = brandEntity.Title,
                CreatedAt = brandEntity.CreatedAt,
                UpdatedAt = brandEntity.UpdatedAt
            };

            return brand;
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }

    public async Task<int> Delete(int id)
    {
        try
        {
            var result = await _catalogBrandRepository.Delete(id);
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
        return await _catalogBrandRepository.Count();
    }
}
