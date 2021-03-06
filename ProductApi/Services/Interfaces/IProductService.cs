using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductApi.Models.Dtos;

namespace ProductApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetProductById(Guid productId);
        Task<List<ProductDto>> GetAllProducts();
        Task<List<ProductDto>> GetProductsByName(string name);
        Task<ProductDto> CreateProduct(ProductDto product);
        Task<ProductDto> UpdateProduct(ProductDto product);
        Task DeleteProduct(Guid id);
    }
}