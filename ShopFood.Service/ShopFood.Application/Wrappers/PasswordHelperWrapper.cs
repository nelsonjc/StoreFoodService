using ShopFood.Domain.Helpers;
using ShopFood.Domain.Interfaces.Application.Wrappers;

namespace ShopFood.Application.Wrappers
{
    public class PasswordHelperWrapper : IPasswordHelper
    {
        public string CreatePasswordHash(string password) => PasswordHelper.CreatePasswordHash(password);

        public bool VerifyPasswordHash(string password, string storedHash, string storedSalt) => PasswordHelper.VerifyPasswordHash(password, storedHash, storedSalt);
    }
}
