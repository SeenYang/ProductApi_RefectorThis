using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProductApi.Models.Dtos;
using ProductApi.Repositories;
using ProductApi.Services.Interfaces;

namespace ProductApi.Services.Implementation
{
    /// <summary>
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository repo, ILogger<ProductService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<ProductDto> GetProductById(Guid productId)
        {
            return await _repo.GetProductById(productId);
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            return await _repo.GetAllProducts();
        }

        public async Task<List<ProductDto>> GetProductsByName(string name)
        {
            return await _repo.GetProductsByName(name);
        }

        public async Task<ProductDto> CreateProduct(ProductDto product)
        {
            return await _repo.CreateProduct(product);
        }

        public async Task<ProductDto> UpdateProduct(ProductDto product)
        {
            await ValidateProductId(product.Id);
            return await _repo.UpdateProduct(product);
        }

        public async Task DeleteProduct(Guid id)
        {
            await ValidateProductId(id);
            await _repo.DeleteProduct(id);
        }

        private async Task ValidateProductId(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                var msg = "Product Id can not be empty.";
                _logger.LogError(msg);
                throw new Exception(msg);
            }

            var product = await _repo.GetProductById(productId);

            if (product == null)
            {
                var msg = $"Can not find product with id {productId}.";
                _logger.LogError(msg);
                throw new Exception(msg);
            }
        }
    }
}