using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Models.Entities
{
    public class ProductOption
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Description { get; set; }

        [ForeignKey("ProductProductOptionFk")] public Product Product { get; set; }
    }
}