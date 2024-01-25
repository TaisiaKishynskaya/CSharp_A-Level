namespace Catalog.API.Infrastructure;

public class ModelToResponseMapperProfile : Profile
{
    public ModelToResponseMapperProfile()
    {
        CreateMap<CatalogType, CatalogTypeResponse>();

        CreateMap<CatalogBrand, CatalogBrandResponse>();

        CreateMap<CatalogType, string>().ConvertUsing(src => src.Title);
        CreateMap<CatalogBrand, string>().ConvertUsing(src => src.Title);

        CreateMap<CatalogItem, CatalogItemResponse>()
             .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.Title))
             .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Title));

    }
}

