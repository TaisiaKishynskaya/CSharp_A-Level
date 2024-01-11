using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.DTOs;
using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Models.Requests.DeleteRequests;
using Catalog.Host.Models.Requests.UpdateRequests;
using Catalog.Host.Models.Responses.UpdateResponses;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _catalogTypeRepository;
        private readonly IMapper _mapper;

        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogTypeRepository catalogTypeRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogTypeRepository = catalogTypeRepository;
            _mapper = mapper;
        }

        async Task<PaginatedItems<CatalogTypeDto>> ICatalogTypeService.GetByPageAsyncHttpGet(int pageIndex, int pageSize)
        {
        var result = await _catalogTypeRepository.GetByPageAsyncHttpGet(pageIndex, pageSize);

            var data = result.Data.Select(item => new CatalogTypeDto
            {
                Id = item.Id,
                Type = item.Type
            });

            return new PaginatedItems<CatalogTypeDto>
            {
                TotalCount = result.TotalCount,
                Data = data
            };
        }

        public Task<int?> AddAsync(AddCatalogTypeRequest addCatalogType)
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.AddAsync(addCatalogType));
        }

        public async Task<UpdateCatalogTypeResponse<int>> UpdateAsync(UpdateCatalogTypeRequest updateCatalogType)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var type = await _catalogTypeRepository.UpdateAsync(updateCatalogType);
                return new UpdateCatalogTypeResponse<int>
                {
                    Type = _mapper.Map<CatalogTypeDto>(type)
                };
            });
        }

        public Task DeleteAsync(DeleteCatalogTypeRequest deleteCatalogType)
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.DeleteAsync(deleteCatalogType));
        }

    }
}