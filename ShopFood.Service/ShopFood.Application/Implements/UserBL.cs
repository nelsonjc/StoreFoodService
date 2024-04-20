using AutoMapper;
using ShopFood.Application.Validations;
using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces.Application.Implements;
using ShopFood.Domain.Interfaces.Application.Wrappers;
using ShopFood.Domain.Interfaces.Repository;

namespace ShopFood.Application.Implements
{
    /// <summary>
    /// Class to User Business Logic
    /// </summary>
    public class UserBL : IUserBL
    {
        #region Variables
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHelper _passwordHelper; 
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor to User business logic
        /// </summary>
        /// <param name="mapper">Parameter mapper type of AutoMapper</param>
        /// <param name="userRepository">Parameter type of Repository to get and set data base</param>
        /// <param name="passwordHelper">Parameter type of passwor helper</param>
        public UserBL(IMapper mapper, IUserRepository userRepository, IPasswordHelper passwordHelper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        } 
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to delete a user
        /// </summary>
        /// <param name="id">Parameter to identify the user to delete</param>
        public async Task DeleteAsync(Guid id) => await this._userRepository.DeleteAsync(id);

        /// <summary>
        /// Method to get all users
        /// </summary>
        /// <returns>List of User result</returns>
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var result = await _userRepository.GetAllAsync();
            if (result != null && result.Count() > 0)
                return _mapper.Map<IEnumerable<UserDto>>(result);

            return null;
        }

        /// <summary>
        /// Method to get a user by id
        /// </summary>
        /// <param name="id">Parameter to identify the user to get</param>
        /// <returns>User Data Info Result</returns>
        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var result = await _userRepository.GetByIdAsync(id);
            if (result != null)
                return _mapper.Map<UserDto>(result);

            return null;
        }

        /// <summary>
        /// Method to create a user
        /// </summary>
        /// <param name="entity">Parameter with user information to create</param>
        public async Task InsertAsync(UserRequest entity)
        {
            UserValidation.UserCreateValidate(entity);
            var userData = _mapper.Map<User>(entity);
            string password = _passwordHelper.CreatePasswordHash(entity.Password);
            userData.PasswordSalt = password.Split(':')[0];
            userData.PasswordHash = password.Split(':')[1];
            await _userRepository.InsertAsync(userData);
        }

        /// <summary>
        /// Method to update a user
        /// </summary>
        /// <param name="entity">Parameter with user information to update</param>
        public async Task UpdateAsync(UserRequest entity)
        {
            UserValidation.UserUpdateValidate(entity);
            var userData = _mapper.Map<User>(entity);
            await _userRepository.UpdateAsync(userData);
        } 
        #endregion
    }
}
