namespace ShopFood.Domain.DTOs.Results
{
    public class UserAuthenticationResultDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public RolDto Rol { get; set; }
        public string Token { get; set; }
    }

    public class RolDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
