using AutoMapper;
using ProductApi.Models.Dtos;
using ProductApi.Models.Entities;

namespace ProductApi.Helpers.Mapper
{
    public class ProductOptionProfile : Profile
    {
        public ProductOptionProfile()
        {
            CreateMap<ProductOption, ProductOptionDto>().ReverseMap();
        }
    }
}