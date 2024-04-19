﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFood.Domain.DTOs.Requests
{
    public class FoodCatalogRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid UserCreatedId { get; set; }
    }
}