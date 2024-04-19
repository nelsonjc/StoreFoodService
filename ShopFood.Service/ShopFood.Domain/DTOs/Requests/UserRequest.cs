using System.ComponentModel.DataAnnotations;

namespace ShopFood.Domain.DTOs.Requests
{
    public class UserRequest
    {
        public Guid? Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Password { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public bool? Active { get; set; }
    }
}
