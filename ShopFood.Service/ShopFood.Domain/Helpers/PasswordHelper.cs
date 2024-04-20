using System.Security.Cryptography;
using System.Text;

namespace ShopFood.Domain.Helpers
{
    /// <summary>
    /// Class to password encrypt
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Method to create a password encrypted
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string CreatePasswordHash(string password)
        {
            ArgumentNullException.ThrowIfNull(password);
            ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));
            byte[] passwordSalt, passwordHash;

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            return $"{Convert.ToBase64String(passwordSalt)}:{Convert.ToBase64String(passwordHash)}";
        }

        /// <summary>
        /// Method to Verify Password Hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        public static bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            ArgumentNullException.ThrowIfNull(password);
            ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));
            ArgumentNullException.ThrowIfNull(storedHash);
            ArgumentNullException.ThrowIfNull(storedSalt);

            byte[] passwordSalt = Convert.FromBase64String(storedSalt);
            byte[] passwordHash = Convert.FromBase64String(storedHash);

            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }
    }
}
