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
    public class UserBL : IUserBL
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHelper _passwordHelper;

        public UserBL(IMapper mapper, IUserRepository userRepository, IPasswordHelper passwordHelper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        }
        public async Task DeleteAsync(Guid id) => await this._userRepository.DeleteAsync(id);

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var result = await _userRepository.GetAllAsync();
            if (result != null && result.Count() > 0)
                return _mapper.Map<IEnumerable<UserDto>>(result);

            return null;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var result = await _userRepository.GetByIdAsync(id);
            if (result != null)
                return _mapper.Map<UserDto>(result);

            return null;
        }

        public async Task InsertAsync(UserRequest entity)
        {
            UserValidation.UserCreateValidate(entity);
            var userData = _mapper.Map<User>(entity);
            string password = _passwordHelper.CreatePasswordHash(entity.Password);
            userData.PasswordSalt = password.Split(':')[0];
            userData.PasswordHash = password.Split(':')[1];
            await _userRepository.InsertAsync(userData);
        }

        public async Task UpdateAsync(UserRequest entity)
        {
            UserValidation.UserUpdateValidate(entity);
            var userData = _mapper.Map<User>(entity);
            await _userRepository.UpdateAsync(userData);
        }
    }
}
