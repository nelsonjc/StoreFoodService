using ShopFood.Domain.Helpers;
using NUnit.Framework;

namespace ShopFood.Tests.Domain.Helpers
{
    [TestFixture]
    public class PasswordHelperTests
    {
        [Test]
        public void CreatePasswordHash_ValidPassword_ReturnsValidHashAndSalt()
        {
            // Arrange
            string password = "password123";

            // Act
            string hashedPassword = PasswordHelper.CreatePasswordHash(password);
            string[] parts = hashedPassword.Split(':');

            // Assert
            Assert.That(parts.Length, Is.EqualTo(2));
            Assert.IsNotNull(parts[0]);
            Assert.IsNotNull(parts[1]);
        }

        [Test]
        public void VerifyPasswordHash_ValidPassword_ReturnsTrue()
        {
            // Arrange
            string password = "password123";
            string hashedPassword = PasswordHelper.CreatePasswordHash(password);
            string[] parts = hashedPassword.Split(':');
            string storedSalt = parts[0];
            string storedHash = parts[1];

            // Act
            bool result = PasswordHelper.VerifyPasswordHash(password, storedHash, storedSalt);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void VerifyPasswordHash_InvalidPassword_ReturnsFalse()
        {
            // Arrange
            string password = "password123";
            string wrongPassword = "wrongpassword";
            string hashedPassword = PasswordHelper.CreatePasswordHash(password);
            string[] parts = hashedPassword.Split(':');
            string storedSalt = parts[0];
            string storedHash = parts[1];

            // Act
            bool result = PasswordHelper.VerifyPasswordHash(wrongPassword, storedHash, storedSalt);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
