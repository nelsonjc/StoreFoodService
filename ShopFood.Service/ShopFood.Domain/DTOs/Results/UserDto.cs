namespace ShopFood.Domain.DTOs.Results
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Active { get; set; }
    }
}
