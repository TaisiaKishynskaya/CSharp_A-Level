namespace Catalog.API.Infrastructure;

public class RequestToModelMapperProfile : Profile
{
    public RequestToModelMapperProfile()
    {
        CreateMap<CatalogTypeRequest, CatalogType>();
        CreateMap<CatalogType, CatalogTypeRequest>();

        CreateMap<CatalogBrandRequest, CatalogBrand>();
        CreateMap<CatalogBrand, CatalogBrandRequest>();

        CreateMap<CatalogItemRequest, CatalogItem>();
        CreateMap<CatalogItem, CatalogItemRequest>();
    }
}
