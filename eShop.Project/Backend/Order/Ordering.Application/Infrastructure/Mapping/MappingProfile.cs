namespace Ordering.Application.Infrastructure.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserEntity, User>().ReverseMap();

        CreateMap<OrderEntity, Order>()
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Items.Sum(i => i.Price * i.Quantity)))
            .ReverseMap();

        CreateMap<OrderItemEntity, OrderItem>().ReverseMap();
    }
}
