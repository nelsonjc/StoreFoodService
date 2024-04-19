namespace ShopFood.Domain.DTOs.Results
{
    public class UserAuthenticationResultDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
