using ShopFood.Domain.Entities;

namespace ShopFood.Domain.Interfaces.Repository
{
    /// <summary>
    /// Interface to user data access
    /// </summary>
    public interface IUserRepository : IGenericBase<User, User>
    {
        Task<User?> GetUserByUsernameAsync(string userName);
        Task InsertCustomerAsync(User entity);
    }
}
