namespace Ordering.API.Infrastructure.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OrderItemUpdateRequest, OrderItemEntity>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<OrderItemUpdateRequest, OrderItem>();

        CreateMap<OrderUpdateRequest, Order>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}
