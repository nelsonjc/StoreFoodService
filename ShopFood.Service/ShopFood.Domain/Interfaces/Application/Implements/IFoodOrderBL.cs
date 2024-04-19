using ShopFood.Domain.DTOs.Requests;

namespace ShopFood.Domain.Interfaces.Application.Implements
{
    public interface IFoodOrderBL
    {
        Task InsertAsync(FoodOrderRequest request);
    }
}
