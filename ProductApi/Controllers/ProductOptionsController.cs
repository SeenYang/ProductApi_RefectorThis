using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models.Dtos;
using ProductApi.Models.Entities;
using ProductApi.Services;
using ProductApi.Services.Interfaces;

namespace ProductApi.Controllers
{
    [Route("api/products/{productId}/options")]
    [ApiController]
    public class ProductOptionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductOptionService _productOptionService;
        private readonly IProductService _productService;

        public ProductOptionsController(IProductService productService, IProductOptionService productOptionService,
            IMapper mapper)
        {
            _productService = productService;
            _productOptionService = productOptionService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOptions(Guid productId)
        {
            if (await _productService.GetProductById(productId) == null) return NotFound("Product not found");

            var productOptions = await _productOptionService.GetAllProductOptionsByProductId(productId);

            return Ok(productOptions);
        }

        [HttpGet("{id}", Name = "GetProductOption")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOption(Guid productId, Guid id)
        {
            if (await _productService.GetProductById(productId) == null) return NotFound("Product not found");

            var productOption = await _productOptionService.GetProductOptionById(id);

            if (productOption == null) return NotFound("Product option not found");
            
            return Ok(productOption);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateOption(Guid productId, [FromBody] ProductOptionDto option)
        {
            if (await _productService.GetProductById(productId) == null) return NotFound("Product not found");

            // todo: validate productId vs option.productId
            var createdOption = await _productOptionService.CreateProductOption(option);
            return Ok(createdOption);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOption(Guid productId, Guid id, ProductOptionDto option)
        {
            if (await _productService.GetProductById(productId) == null) return NotFound("Product not found");

            if (await _productOptionService.GetProductOptionById(id) == null)
                return NotFound("Product option not found");

            var updatedProductOption = await _productOptionService.UpdateProductOption(option);
            return Ok(updatedProductOption);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOption(Guid productId, Guid id)
        {
            if (await _productService.GetProductById(productId) == null) return NotFound("Product not found");

            if (await _productOptionService.GetProductOptionById(id) == null)
                return NotFound("Product option not found");

            await _productOptionService.DeleteProductOption(productId, id);
            return Ok();
        }
    }
}