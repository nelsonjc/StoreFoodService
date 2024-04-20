using ShopFood.Domain.Helpers;
using ShopFood.Domain.Interfaces.Application.Wrappers;

namespace ShopFood.Application.Wrappers
{
    /// <summary>
    /// Class to Password Helper Wrapper
    /// </summary>
    public class PasswordHelperWrapper : IPasswordHelper
    {
        /// <summary>
        /// Method to Create Password Hash
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string CreatePasswordHash(string password) => PasswordHelper.CreatePasswordHash(password);

        /// <summary>
        /// Method to Verify Password Hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        public bool VerifyPasswordHash(string password, string storedHash, string storedSalt) => PasswordHelper.VerifyPasswordHash(password, storedHash, storedSalt);
    }
}
