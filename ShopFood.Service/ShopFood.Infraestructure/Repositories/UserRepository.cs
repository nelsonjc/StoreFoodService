using Microsoft.Extensions.Configuration;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces.Repository;

namespace ShopFood.Infraestructure.Repositories
{
    public class UserRepository : DbContext, IUserRepository
    {

        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByUsernameAsync(string userName) => await this.GetAsyncFirst<User?>($"EXEC [dbo].[SP_User_GetBy_UserName] '{userName}'");

        public void Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
