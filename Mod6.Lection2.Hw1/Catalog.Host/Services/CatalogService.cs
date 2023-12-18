using AutoMapper;
using Catalog.Host.Configurations;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly IMapper _mapper;

    public CatalogService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize);
            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public async Task<CatalogItemDto> GetByIdAsync(int id)
    {
        var result = await _catalogItemRepository.GetByIdAsync(id);

        return _mapper.Map<CatalogItemDto>(result);
    }

    public Task<IEnumerable<CatalogItemDto>> GetByBrandAsync(string brand)
    {
        var result = _catalogItemRepository.GetByBrandAsync(brand);

        return Task.FromResult(result.Result.Select(s => _mapper.Map<CatalogItemDto>(s)));
    }

    public Task<IEnumerable<CatalogItemDto>> GetByTypeAsync(string type)
    {
        var result = _catalogItemRepository.GetByTypeAsync(type);

        return Task.FromResult(result.Result.Select(s => _mapper.Map<CatalogItemDto>(s)));
    }
}