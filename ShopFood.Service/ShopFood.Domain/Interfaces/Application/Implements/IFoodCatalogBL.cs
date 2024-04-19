using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFood.Domain.Interfaces.Application.Implements
{
    public interface IFoodCatalogBL : IGenericBase<FoodCatalogDto, FoodCatalogRequest>
    {
        Task<IEnumerable<FoodCatalogCustomerDto>> GetAllCustomerAsync();
    }
}
