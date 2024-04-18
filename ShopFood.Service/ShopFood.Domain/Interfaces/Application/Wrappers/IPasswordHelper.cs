namespace ShopFood.Domain.Interfaces.Application.Wrappers
{
    public interface IPasswordHelper
    {
        string CreatePasswordHash(string password);
        bool VerifyPasswordHash(string password, string storedHash, string storedSalt);
    }
}

