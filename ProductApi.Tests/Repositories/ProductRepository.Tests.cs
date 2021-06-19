using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ProductApi.Helpers;
using ProductApi.Helpers.Mapper;
using ProductApi.Models.Dtos;
using ProductApi.Models.Entities;
using ProductApi.Repositories;
using ProductApi.Repositories.Implementation;
using Xunit;

namespace ProductApi.Tests.Repositories
{
    public class ProductRepository_Tests
    {
        private readonly Guid _productId1 = Guid.NewGuid();
        private readonly Guid _productId2 = Guid.NewGuid();
        private readonly Guid _productId3 = Guid.NewGuid();
        private readonly Guid _productId4 = Guid.NewGuid();

        private readonly IProductRepository _repo;
        private readonly ProductsContext _context;

        public ProductRepository_Tests()
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new ProductProfile()); });
            var mapper = mappingConfig.CreateMapper();
            var logger = new Mock<ILogger<ProductRepository>>();

            var options = new DbContextOptionsBuilder<ProductsContext>()
                .UseInMemoryDatabase(databaseName: "ImMemoryDB")
                .Options;
            _context = new ProductsContext(options);
            var activeProduct = new Product
            {
                Id = _productId2,
                Name = "active product for update",
                Price = 1.99m,
                DeliveryPrice = 0.3m,
                Description = "product for update test.",
                Status = (int) ProductStatusEnum.Active
            };

            var inactiveProduct = new Product
            {
                Id = _productId3,
                Name = "Inactive product for update",
                Price = 1.99m,
                DeliveryPrice = 0.3m,
                Description = "product for update test.",
                Status = (int) ProductStatusEnum.Inactive
            };

            var productForDelete = new Product
            {
                Id = _productId4,
                Name = "Product for delete test",
                Price = 19.99m,
                DeliveryPrice = 1.12m,
                Description = "New product for test. Delete",
                Status = (int) ProductStatusEnum.Inactive
            };
            _context.Products.Add(activeProduct);
            _context.Products.Add(inactiveProduct);
            _context.Products.Add(productForDelete);
            _context.SaveChanges();
            _repo = new ProductRepository(_context, mapper, logger.Object);
        }

        #region GetProductById

        [Fact(DisplayName = "GetProductById no data match")]
        public async void Test1()
        {
            var result = await _repo.GetProductById(_productId1);
            Assert.Null(result);
        }

        [Fact(DisplayName = "GetProductById with data match")]
        public async void Test2()
        {
            var result = await _repo.GetProductById(_productId2);
            Assert.NotNull(result);
            Assert.Equal(_productId2, result.Id);
        }

        [Fact(DisplayName = "GetProductById data match but not active")]
        public async void Test3()
        {
            var result = await _repo.GetProductById(_productId3);
            Assert.Null(result);
        }

        #endregion GetProductById

        #region CreateProduct

        [Fact(DisplayName = "Create Product")]
        public async void Create_Test1()
        {
            var newProduct = new ProductDto
            {
                Name = "NewProduct0",
                Price = 9.99m,
                DeliveryPrice = 0.12m,
                Description = "New product for test."
            };

            var result = await _repo.CreateProduct(newProduct);

            var verifyResult = await _context.Products.FirstOrDefaultAsync(p => p.Id == result.Id);
            Assert.NotNull(result);
            Assert.NotNull(verifyResult);
            Assert.Equal(result.Name, verifyResult.Name);
            Assert.Equal(result.DeliveryPrice, verifyResult.DeliveryPrice);
            Assert.Equal(result.Price, verifyResult.Price);
            Assert.Equal(result.Description, verifyResult.Description);
        }

        #endregion endregion

        #region UpdateProduct

        [Fact(DisplayName = "Update Product")]
        public async void Update_Test1()
        {
            var target = new ProductDto
            {
                Id = _productId2,
                Name = "Product Update",
                Price = 19.99m,
                DeliveryPrice = 1.12m,
                Description = "New product for test. Updated"
            };

            var result = await _repo.UpdateProduct(target);

            var verifyResult = await _context.Products.FirstOrDefaultAsync(p => p.Id == _productId2);
            Assert.NotNull(result);
            Assert.NotNull(verifyResult);
            Assert.Equal(verifyResult.Name, verifyResult.Name);
            Assert.Equal(verifyResult.Price, verifyResult.Price);
            Assert.Equal(verifyResult.DeliveryPrice, verifyResult.DeliveryPrice);
        }

        [Fact(DisplayName = "Update Product - Product not found")]
        public async void Update_Test2()
        {
            var target = new ProductDto
            {
                Id = Guid.NewGuid(),
                Name = "Product Update",
                Price = 19.99m,
                DeliveryPrice = 1.12m,
                Description = "New product for test. Updated"
            };

            var exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _repo.UpdateProduct(target);
            });

            Assert.NotNull(exception);
            Assert.Equal($"Fail to update product {target.Id}.", exception.Message);
            Assert.Equal($"Can not find product {target.Id} during update.", exception.InnerException?.Message);
        }

        [Fact(DisplayName = "Update Product - Product not active")]
        public async void Update_Test3()
        {
            var target = new ProductDto
            {
                Id = _productId3,
                Name = "Product Update",
                Price = 19.99m,
                DeliveryPrice = 1.12m,
                Description = "New product for test. Updated"
            };

            var exception = await Record.ExceptionAsync(async () =>
            {
                var result = await _repo.UpdateProduct(target);
            });

            Assert.NotNull(exception);
            Assert.Equal($"Fail to update product {target.Id}.", exception.Message);
            Assert.Equal($"Can not update product {target.Id} due to it's inactive.",
                exception.InnerException?.Message);
        }

        #endregion

        #region Delete Product

        [Fact(DisplayName = "Delete Product")]
        public async void Delete_Test1()
        {
            var exception = await Record.ExceptionAsync(async () => { await _repo.DeleteProduct(_productId4); });

            Assert.Null(exception);

            var verifyResult = await _context.Products.FirstOrDefaultAsync(p => p.Id == _productId4);
            Assert.NotNull(verifyResult);
            Assert.Equal((int) ProductStatusEnum.Inactive, verifyResult.Status);
        }

        [Fact(DisplayName = "Delete Product - product can't find")]
        public async void Delete_Test2()
        {
            var fakeGuid = Guid.NewGuid();
            var exception = await Record.ExceptionAsync(async () => { await _repo.DeleteProduct(fakeGuid); });

            Assert.NotNull(exception);
            Assert.Equal($"Fail to remove product {fakeGuid}.", exception.Message);
            Assert.Equal($"Can not find product {fakeGuid} during deleting.",
                exception.InnerException?.Message);
        }

        #endregion endregion
    }
}