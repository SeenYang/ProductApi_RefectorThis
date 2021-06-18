﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models.Entities
{
    public class Product
    {
        [Key] public Guid Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Description { get; set; }

        [Required] public decimal Price { get; set; }

        [Required] public decimal DeliveryPrice { get; set; }

        public List<ProductOption> ProductOptions { get; set; }
    }
}