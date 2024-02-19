namespace Catalog.API.Infrastructure.Mapping;

// код представляет собой класс RequestToModelMapperProfile, который является профилем AutoMapper.
// AutoMapper - это библиотека для автоматического сопоставления полей и свойств между объектами разных типов.
// В этом профиле определены отображения (mapping) между объектами запросов (Request) и моделями данных (Model).
// Каждое отображение создается с помощью метода CreateMap<>, который определяет сопоставление между типами.

public class RequestToModelMapperProfile : Profile
{
    public RequestToModelMapperProfile()
    {
        // десь определяется сопоставление между объектом запроса типа CatalogTypeRequest и моделью данных типа CatalogType.
        // .ReverseMap() указывает, что это отображение является обратным, то есть также можно выполнять сопоставление в обратном направлении, из CatalogType в CatalogTypeRequest.
        CreateMap<CatalogTypeRequest, CatalogType>().ReverseMap();

        CreateMap<CatalogBrandRequest, CatalogBrand>().ReverseMap();

        CreateMap<CatalogItemRequest, CatalogItem>().ReverseMap();
    }
}
