using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.DTOs;
using Catalog.Host.Models.Requests.AddRequests;
using Catalog.Host.Models.Requests.DeleteRequests;
using Catalog.Host.Models.Requests.UpdateRequests;
using Catalog.Host.Models.Responses.UpdateResponses;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly IMapper _mapper;

        public CatalogItemService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogItemRepository catalogItemRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogItemRepository = catalogItemRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<CatalogGetItemDto>> GetByPageAsyncHttpGet(int pageIndex, int pageSize)
        {
            var result = await _catalogItemRepository.GetByPageAsyncHttpGet(pageIndex, pageSize);

            if (result != null)
            {
                var data = result.Data.Select(item => new CatalogGetItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    PictureFileName = item.PictureFileName,
                    BrandName = item.CatalogBrand.Brand,
                    TypeName = item.CatalogType.Type
                });

                return new PaginatedItems<CatalogGetItemDto>
                {
                    TotalCount = result.TotalCount,
                    Data = data
                };
            }

            return null;
        }

        public Task<int?> AddAsync(AddCatalogItemRequest addCatalogItem)
        {
            return ExecuteSafeAsync(() => _catalogItemRepository.AddAsync(addCatalogItem));
        }

        public async Task<UpdateCatalogItemResponse<int>> UpdateAsync(UpdateCatalogItemRequest updateCatalogItem)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var item = await _catalogItemRepository.UpdateAsync(updateCatalogItem);
                return new UpdateCatalogItemResponse<int>
                {                  
                    Item = _mapper.Map<CatalogGetItemDto>(item)
                };
            });
        }

        public Task DeleteAsync(DeleteCatalogItemRequest deleteCatalogItem)
        {
            return ExecuteSafeAsync(() => _catalogItemRepository.DeleteAsync(deleteCatalogItem));
        }


    }
}