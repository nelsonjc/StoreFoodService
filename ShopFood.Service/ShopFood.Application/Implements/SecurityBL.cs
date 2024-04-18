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

namespace ShopFood.Application.Implements
{
    public class SecurityBL : ISecurityBL
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IConfiguration _configuration;

        public SecurityBL(IUserRepository userRepository, IPasswordHelper passwordHelper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _configuration = configuration;
        }

        public async Task<UserAuthenticationResultDto> AuthenticateAsync(LoginRequest model)
        {
            var user = await _userRepository.GetUserByUsernameAsync(model.Username);
            if (user == null || !_passwordHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return null; // Autenticación fallida

            var token = GenerateJwtToken(user);
            return new UserAuthenticationResultDto { Token = token };
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSettings = _configuration.GetSection("Jwt");
            var secret = jwtSettings["Secret"];
            var expirationInMinutes = jwtSettings["ExpirationInMinutes"];

            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(expirationInMinutes)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
