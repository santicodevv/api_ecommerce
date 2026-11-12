using System;
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using AutoMapper;

namespace ApiEcommerce.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
       CreateMap<ProductDTO, Product>().ReverseMap()
        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
       CreateMap<CreateProductDTO, Product>().ReverseMap();
       CreateMap<ProductProductDTO, Product>().ReverseMap();
    }
}
