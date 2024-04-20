using Microsoft.Extensions.Configuration;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces.Repository;
using SP = ShopFood.Domain.Variables.StoreProcedures;

namespace ShopFood.Infraestructure.Repositories
{
    /// <summary>
    /// Class to access to data of User
    /// </summary>
    public class UserRepository : DbContext, IUserRepository
    {
        #region Ctor
        /// <summary>
        /// Ctro to User Repository
        /// </summary>
        /// <param name="configuration">Parameter to get information from app settings type of Microsoft.Extensions.Configuration </param>
        public UserRepository(IConfiguration configuration) : base(configuration) { }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method to delete a user
        /// </summary>
        /// <param name="id">Parameter to identify the user to delete</param>
        public async Task DeleteAsync(Guid id) => await GetAsyncFirst<User?>($"{SP.EXEC} {SP.User_Delete} '{id}'");

        /// <summary>
        /// Method to get all users
        /// </summary>
        /// <returns>List of User result</returns>
        public async Task<IEnumerable<User>> GetAllAsync() => await ExecuteQuery<User>($"{SP.EXEC} {SP.User_GetAll}");

        /// <summary>
        /// Method to get a user by id
        /// </summary>
        /// <param name="id">Parameter to identify the user to get</param>
        /// <returns>User Data Info Result</returns>
        public async Task<User> GetByIdAsync(Guid id) => await GetAsyncFirst<User>($"{SP.EXEC} {SP.User_GetBy_Id} '{id}'");

        /// <summary>
        /// Method to get user by user name
        /// </summary>
        /// <param name="userName">Parameter to identify a user by username</param>
        /// <returns>A User Data Info Result</returns>
        public async Task<User> GetUserByUsernameAsync(string userName) => await GetAsyncFirst<User>($"{SP.EXEC} {SP.User_GetBy_UserName} '{userName}'");

        /// <summary>
        /// Method to create a user
        /// </summary>
        /// <param name="entity">Parameter with user information to create</param>
        public async Task InsertAsync(User entity)
        {
            var parameters = new
            {
                Name = entity.Name,
                UserName = entity.Username,
                PasswordHash = entity.PasswordHash,
                PasswordSalt = entity.PasswordSalt,
                IdRole = entity.RoleId
            };

            await ExecuteCommand(SP.User_Insert, parameters);
        }

        /// <summary>
        /// Method to update a user
        /// </summary>
        /// <param name="entity">Parameter with user information to update</param>
        public async Task UpdateAsync(User entity)
        {
            var parameters = new
            {
                IdUser = entity.Id,
                Name = entity.Name,
                UserName = entity.Username,
                IdRole = entity.RoleId,
                Active = entity.Active
            };
            await ExecuteCommand(SP.User_Update, parameters);
        } 
        #endregion
    }
}
