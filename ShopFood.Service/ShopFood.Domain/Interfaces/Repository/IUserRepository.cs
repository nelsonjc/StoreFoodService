using ShopFood.Domain.Entities;

namespace ShopFood.Domain.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUsernameAsync(string userName);
    }
}
