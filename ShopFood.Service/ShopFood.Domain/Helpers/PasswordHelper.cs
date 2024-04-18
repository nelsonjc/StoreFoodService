using System.Security.Cryptography;
using System.Text;

namespace ShopFood.Domain.Helpers
{
    public static class PasswordHelper
    {
        public static string CreatePasswordHash(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("El valor no puede estar vacío o contener solo espacios en blanco.", nameof(password));

            byte[] passwordSalt;
            byte[] passwordHash;

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            return $"{Convert.ToBase64String(passwordSalt)}:{Convert.ToBase64String(passwordHash)}";
        }

        public static bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("El valor no puede estar vacío o contener solo espacios en blanco.", nameof(password));
            if (storedHash == null) throw new ArgumentNullException(nameof(storedHash));
            if (storedSalt == null) throw new ArgumentNullException(nameof(storedSalt));

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
