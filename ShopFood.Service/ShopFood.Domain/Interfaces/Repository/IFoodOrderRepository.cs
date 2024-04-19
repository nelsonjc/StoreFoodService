using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.Entities;

namespace ShopFood.Domain.Interfaces.Repository
{
    public interface IFoodOrderRepository
    {
        Task<FoodOrderHead> InsertAsync(FoodOrderRequest request);
    }
}
