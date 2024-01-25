namespace Catalog.Application.Infrastructure;

public class EntityToModelMapperProfile : Profile
{
    public EntityToModelMapperProfile()
    {
        CreateMap<CatalogTypeEntity, CatalogType>();

        CreateMap<CatalogType, CatalogTypeEntity>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<CatalogBrandEntity, CatalogBrand>();

        CreateMap<CatalogBrand, CatalogBrandEntity>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<CatalogItemEntity, CatalogItem>()
           .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
           .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand));

        CreateMap<CatalogItem, CatalogItemEntity>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
    }
}
