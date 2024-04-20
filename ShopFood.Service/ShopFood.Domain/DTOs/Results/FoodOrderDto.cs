using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFood.Domain.DTOs.Results
{
    /// <summary>
    /// Class to Food order head data object
    /// </summary>
    public class FoodOrderHeadDto
    {
        public Guid Id { get; set; }
        public int FoodQuantity { get; set; }
        public Guid StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusCode { get; set; }
        public Guid UserCreatedId { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public List<FoodOrderDetailDto> Details { get; set; }
    }

    /// <summary>
    /// Class to Food order detail data object
    /// </summary>
    public class FoodOrderDetailDto
    {
        public int ItemNumber { get; set; }
        public Guid Id { get; set; }
        public Guid FoodCatalogId { get; set; }
        public string FoodCatalogName { get; set; }
        public string FoodCatalogDescription { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public bool Active { get; set; }
    }
}
