using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;

namespace ShopFood.Domain.Interfaces.Application.Implements
{
    public interface ISecurityBL
    {
        Task<UserAuthenticationResultDto> AuthenticateAsync(LoginRequest model);
    }
}
