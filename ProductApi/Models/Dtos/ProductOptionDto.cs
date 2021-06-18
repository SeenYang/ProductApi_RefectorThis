using System;

namespace ProductApi.Models.Dtos
{
    public class ProductOptionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ProductId { get; set; }
    }
}