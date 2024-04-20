using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;

namespace ShopFood.Domain.Interfaces.Application.Implements
{
    /// <summary>
    /// Interface to Food Order bussiness logic
    /// </summary>
    public interface IFoodOrderBL : IGenericBase<FoodOrderHeadDto, FoodOrderRequest>
    {
        /// <summary>
        /// Method to do confirm a food order
        /// </summary>
        /// <param name="id">Paremeter type of Guid with id food order head</param>
        /// <returns>Html string content with reesponse</returns>
        Task<string> ConfirmAsync(Guid id);
    }
}
