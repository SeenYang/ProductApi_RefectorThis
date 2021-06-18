using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductApi.Models.Dtos;
using ProductApi.Models.Entities;

namespace ProductApi.Repositories
{
    public interface IProductRepository
    {
        Task<ProductDto> GetProductById(Guid productId);
        Task<List<ProductDto>> GetAllProducts();
        Task<ProductDto> CreateProduct(Product product);
        Task<ProductDto> UpdateProduct(Product product);
        Task DeleteProduct(Guid id);
    }
}