using Application.Dtos.Product;
using AutoMapper;
using Domain.Entities;

namespace Application.Map;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Products, ProductDto>();
        CreateMap<ProductDto, Products>();
    }
}