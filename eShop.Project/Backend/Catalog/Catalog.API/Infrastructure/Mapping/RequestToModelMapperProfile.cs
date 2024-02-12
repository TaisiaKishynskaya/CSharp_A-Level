namespace Catalog.API.Infrastructure.Mapping;

public class RequestToModelMapperProfile : Profile
{
    public RequestToModelMapperProfile()
    {
        CreateMap<CatalogTypeRequest, CatalogType>().ReverseMap();

        CreateMap<CatalogBrandRequest, CatalogBrand>().ReverseMap();

        CreateMap<CatalogItemRequest, CatalogItem>().ReverseMap();
    }
}
