using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.Entities;

namespace ShopFood.Domain.Interfaces.Repository
{
    /// <summary>
    /// Interface to food ordery data access info
    /// </summary>
    public interface IFoodOrderRepository : IGenericBase<FoodOrderHead, FoodOrderRequest>
    {
        new Task<FoodOrderHead> InsertAsync(FoodOrderRequest request);
        Task ConfirmAsync(Guid id);
    }
}
