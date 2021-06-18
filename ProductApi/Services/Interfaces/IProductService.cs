using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductApi.Models.Dtos;
using ProductApi.Models.Entities;

namespace ProductApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetProductById(Guid productId);
        Task<List<ProductDto>> GetAllProducts();
        Task<ProductDto> CreateProduct(Product product);
        Task<ProductDto> UpdateProduct(Guid id, Product product);
        Task DeleteProduct(Guid id);
    }
}