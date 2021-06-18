using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductApi.Models.Dtos;
using ProductApi.Models.Entities;
using ProductApi.Repositories.Interfaces;

namespace ProductApi.Repositories.Implementation
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        private readonly ProductsContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductOptionRepository> _logger;

        public ProductOptionRepository(ProductsContext context, ILogger<ProductOptionRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ProductOptionDto> GetProductOptionById(Guid id)
        {
            var option = await _context.ProductOptions.FirstOrDefaultAsync(po => po.Id == id);

            return _mapper.Map<ProductOption, ProductOptionDto>(option);
        }

        public async Task<List<ProductOptionDto>> GetAllProductOptionsByProductId(Guid productId)
        {
            var options = await _context.ProductOptions.Where(po => po.ProductId == productId).ToListAsync();

            return _mapper.Map<List<ProductOption>, List<ProductOptionDto>>(options);
        }

        public async Task<ProductOptionDto> CreateProductOption(ProductOptionDto option)
        {
            var entity = _mapper.Map<ProductOptionDto, ProductOption>(option);

            try
            {
                await _context.ProductOptions.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                HandleException(LogLevel.Error, $"Fail to create product option.", e);
            }

            return _mapper.Map<ProductOption, ProductOptionDto>(entity);
        }

        public async Task<ProductOptionDto> UpdateProductOption(ProductOptionDto option)
        {
            try
            {
                var source = await _context.ProductOptions.FirstOrDefaultAsync(po => po.Id == option.Id);
                if (source == null)
                {
                    throw new Exception($"Can not find product option {option.Id} during update.");
                }

                source.Name = option.Name;
                source.Description = option.Description;
                source.ProductId = option.ProductId;
                // todo: confirm whether need update(source);
                await _context.SaveChangesAsync();

                return _mapper.Map<ProductOption, ProductOptionDto>(source);
            }
            catch (Exception e)
            {
                HandleException(LogLevel.Error, $"Fail to update product option {option.Id}.", e);
            }

            return option;
        }

        public async Task<bool> DeleteProductOption(Guid id)
        {
            try
            {
                var source = await _context.ProductOptions.FirstOrDefaultAsync(po => po.Id == id);
                if (source == null)
                {
                    HandleException(LogLevel.Error, $"Can not find product option {id} during deleting.", null);
                }

                _context.ProductOptions.Remove(source);
            }
            catch (Exception e)
            {
                HandleException(LogLevel.Error, $"Fail to remove product option {id}.", e);
            }

            return true;
        }

        private void HandleException(LogLevel level, string message, Exception e)
        {
            _logger.Log(level, message);
            throw new Exception(message, e ?? new Exception());
        }
    }
}