using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductApi.Models.Dtos;
using ProductApi.Models.Entities;

namespace ProductApi.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        public Task<ProductDto> GetProductById(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDto>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}