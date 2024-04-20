using ShopFood.Domain.DTOs.Requests;

namespace ShopFood.Domain.Interfaces.Application.Implements
{
    /// <summary>
    /// Interface to Food Order bussiness logic
    /// </summary>
    public interface IFoodOrderBL
    {
        /// <summary>
        /// Method to do a food order
        /// </summary>
        /// <param name="request">Parameter type of request data</param>
        Task InsertAsync(FoodOrderRequest request);

        /// <summary>
        /// Method to do confirm a food order
        /// </summary>
        /// <param name="id">Paremeter type of Guid with id food order head</param>
        /// <returns>Html string content with reesponse</returns>
        Task<string> ConfirmAsync(Guid id);
    }
}
