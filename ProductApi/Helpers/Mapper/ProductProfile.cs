using AutoMapper;
using ProductApi.Models.Dtos;
using ProductApi.Models.Entities;

namespace ProductApi.Helpers.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}