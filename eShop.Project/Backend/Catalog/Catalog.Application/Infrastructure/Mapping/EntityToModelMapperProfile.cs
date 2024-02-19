namespace Catalog.Application.Infrastructure.Mapping;

// Этот код определяет класс EntityToModelMapperProfile, который наследует от класса Profile из библиотеки AutoMapper.
// Он предназначен для настройки сопоставления (mapping) между сущностями (Entity) и моделями (Model). 

public class EntityToModelMapperProfile : Profile
{
    // Это конструктор класса, который вызывается при создании экземпляра класса. Внутри происходит настройка сопоставления с помощью вызова метода CreateMap.
    public EntityToModelMapperProfile()
    {
        // Этот метод указывает AutoMapper на необходимость сопоставления между классами CatalogTypeEntity и CatalogType.
        CreateMap<CatalogTypeEntity, CatalogType>(); // AutoMapper автоматически сопоставит поля и свойства с одинаковыми именами.

        // Этот метод определяет дополнительные настройки для сопоставления между CatalogType и CatalogTypeEntity.
        // В данном случае, используется метод ForMember, чтобы игнорировать сопоставление для свойства CreatedAt.
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

// Этот класс предоставляет настройки маппинга, которые затем используются в приложении для автоматического преобразования данных между сущностями БД и моделями представления.
