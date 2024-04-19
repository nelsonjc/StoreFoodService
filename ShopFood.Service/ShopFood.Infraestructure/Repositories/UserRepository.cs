using Microsoft.Extensions.Configuration;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces.Repository;

namespace ShopFood.Infraestructure.Repositories
{
    public class UserRepository : DbContext, IUserRepository
    {

        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public async Task DeleteAsync(Guid id)
        {
            await this.GetAsyncFirst<User?>($"EXEC [dbo].[SP_User_Delete] '{id}'");
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await this.ExecuteQuery<User>($"EXEC [dbo].[SP_User_GetAll]");            
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await this.GetAsyncFirst<User>($"EXEC [dbo].[SP_User_GetBy_Id] '{id}'");
        }

        public async Task<User?> GetUserByUsernameAsync(string userName) => await this.GetAsyncFirst<User?>($"EXEC [dbo].[SP_User_GetBy_UserName] '{userName}'");

        public async Task InsertAsync(User entity)
        {
            var parameters = new { Name = entity.Name, UserName = entity.Username, PasswordHash = entity.PasswordHash, PasswordSalt = entity.PasswordSalt, IdRole = entity.RoleId };
            await this.ExecuteCommand("[dbo].[SP_User_Insert]", parameters);
        }

        public async Task UpdateAsync(User entity)
        {
            var parameters = new { IdUser = entity.Id, Name = entity.Name, UserName = entity.Username, IdRole = entity.RoleId, Active = entity.Active };
            await this.ExecuteCommand($"[dbo].[SP_User_Update]", parameters);            
        }
    }
}
