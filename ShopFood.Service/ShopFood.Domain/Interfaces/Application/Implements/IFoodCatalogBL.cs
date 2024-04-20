using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;

namespace ShopFood.Domain.Interfaces.Application.Implements
{
    /// <summary>
    /// Interface to food catalog business logic
    /// </summary>
    public interface IFoodCatalogBL : IGenericBase<FoodCatalogDto, FoodCatalogRequest>
    {
        Task<IEnumerable<FoodCatalogCustomerDto>> GetAllCustomerAsync();
    }
}
