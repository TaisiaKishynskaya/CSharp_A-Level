using AutoMapper;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.DTOs;

namespace Catalog.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogItem, CatalogItemDto>()
            .ForMember("PictureFileName", opt
                => opt.MapFrom<CatalogItemPictureResolver, string>(c => c.PictureFileName));
        CreateMap<CatalogItem, CatalogGetItemDto>()
            .ForMember(dest => dest.PictureFileName, opt => opt.MapFrom<CatalogGetItemPictureResolver, string>(src => src.PictureFileName))
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.CatalogBrand.Brand))
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.CatalogType.Type));

        CreateMap<CatalogBrand, CatalogBrandDto>();
        CreateMap<CatalogType, CatalogTypeDto>();
    }
}