using ShopFood.Domain.Entities;

namespace ShopFood.Domain.Interfaces.Repository
{
    public interface IUserRepository : IGenericBase<User, User>
    {
        Task<User> GetUserByUsernameAsync(string userName);
    }
}
