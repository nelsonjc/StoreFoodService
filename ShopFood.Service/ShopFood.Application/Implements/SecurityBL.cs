using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces.Application.Implements;
using ShopFood.Domain.Interfaces.Application.Wrappers;
using ShopFood.Domain.Interfaces.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AC = ShopFood.Domain.Variables.AppConfig;

namespace ShopFood.Application.Implements
{
    /// <summary>
    /// Class to Security business logic
    /// </summary>
    public class SecurityBL : ISecurityBL
    {
        #region Variables
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IConfiguration _configuration; 
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor to Food Catalog bussiness logic
        /// </summary>
        /// <param name="mapper">Parameter mapper type of AutoMapper</param>
        /// <param name="userRepository">Parameter type of Repository to get and set data base</param>
        /// <param name="passwordHelper">Parameter type of passwor helper</param>
        /// <param name="configuration">Parameter configuration type of Microsoft.Extensions.Configuration</param>
        public SecurityBL(IMapper mapper, IUserRepository userRepository, IPasswordHelper passwordHelper, IConfiguration configuration)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _configuration = configuration;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method to User Authentication
        /// </summary>
        /// <param name="model">Parameter with data to Login</param>
        /// <returns>User Authenticated or error</returns>
        public async Task<UserAuthenticationResultDto> AuthenticateAsync(LoginRequest model)
        {
            var user = await _userRepository.GetUserByUsernameAsync(model.Username);
            if (user == null || !_passwordHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            UserAuthenticationResultDto userAutheticated = _mapper.Map<UserAuthenticationResultDto>(user);
            userAutheticated.Token = GenerateJwtToken(user);
            return userAutheticated;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Method to generate token with data user 
        /// </summary>
        /// <param name="user">Parameter with data user</param>
        /// <returns>Token value</returns>
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSettings = _configuration.GetSection(AC.AppSetting_JWT_Section);
            var secret = jwtSettings[AC.AppSetting_JWT_Secret];
            var audience = jwtSettings[AC.AppSetting_JWT_Audience];
            var issuer = jwtSettings[AC.AppSetting_JWT_Issuer];
            var expirationInMinutes = jwtSettings[AC.AppSetting_JWT_ExpirationInMinutes];

            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.RoleName)
                }),
                Audience = audience,
                Issuer = issuer,
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(expirationInMinutes)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        } 
        #endregion
    }
}
