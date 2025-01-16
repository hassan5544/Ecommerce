using Application.Dtos.Order;
using AutoMapper;
using Domain.Entities;

namespace Application.Map;

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<OrderItems, OrderItemDto>();
        CreateMap<OrderItemDto, OrderItems>();
        
    }
}