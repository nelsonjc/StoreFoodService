using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;

namespace ShopFood.Domain.Interfaces.Application.Implements
{
    /// <summary>
    /// Interface to security business logic
    /// </summary>
    public interface ISecurityBL
    {
        Task<UserAuthenticationResultDto> AuthenticateAsync(LoginRequest model);
    }
}
