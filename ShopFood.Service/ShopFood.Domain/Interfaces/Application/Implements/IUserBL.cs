using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;

namespace ShopFood.Domain.Interfaces.Application.Implements
{
    /// <summary>
    /// Insterface to User business logic
    /// </summary>
    public interface IUserBL : IGenericBase<UserDto, UserRequest>
    {
        Task InsertCustomerAsync(UserRequest entity);
    }
}
