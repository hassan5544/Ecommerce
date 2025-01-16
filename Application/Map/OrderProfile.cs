using Application.Dtos.Order;
using AutoMapper;
using Domain.Entities;

namespace Application.Map;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Orders, OrderDto>();
        CreateMap<OrderDto, Orders>();
    }
}