using AutoMapper;
using ProductApi.Models.Dtos;
using ProductApi.Models.Entities;

namespace ProductApi.Helpers.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(target => target.ProductOptions,
                    map => map.MapFrom(src => src.ProductOptions))
                .ReverseMap();
            CreateMap<ProductOption, ProductOptionDto>().ReverseMap();
        }
    }
}