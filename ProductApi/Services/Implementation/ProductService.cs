using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProductApi.Models.Dtos;
using ProductApi.Models.Entities;
using ProductApi.Repositories;
using ProductApi.Repositories.Interfaces;
using ProductApi.Services.Interfaces;

namespace ProductApi.Services.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private ILogger<ProductService> _logger;

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

        public async Task<ProductDto> CreateProduct(ProductDto product)
        {
            var newProduct = await _repo.CreateProduct(product);

            var returnResult =  await GetProductById(newProduct.Id);
            
            return returnResult;
        }

        public async Task<ProductDto> UpdateProduct(Guid id, ProductDto product)
        {
            // todo Validation
            return await _repo.UpdateProduct(product);
        }

        public async Task DeleteProduct(Guid id)
        {
            await _repo.DeleteProduct(id);
        }
    }
}