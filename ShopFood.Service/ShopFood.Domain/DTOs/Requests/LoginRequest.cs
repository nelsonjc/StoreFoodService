using System.ComponentModel.DataAnnotations;

namespace ShopFood.Domain.DTOs.Requests
{
    public class LoginRequest
    {
            
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
