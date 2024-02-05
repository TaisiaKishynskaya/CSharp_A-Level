using AutoMapper;
using Ordering.API.Requests;
using Ordering.DataAccess.Entities;
using Ordering.Domain.Models;

namespace Ordering.API.Mapping;

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
