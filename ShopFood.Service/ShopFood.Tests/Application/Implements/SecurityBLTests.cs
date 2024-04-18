using Microsoft.Extensions.Configuration;
using Moq;
using ShopFood.Application.Implements;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces.Application.Wrappers;
using ShopFood.Domain.Interfaces.Repository;
using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.Interfaces.Application.Implements;

namespace ShopFood.Tests.Application.Implements
{
    [TestFixture]
    public class SecurityBLTests
    {
        private ISecurityBL _securityBL;

        [SetUp]
        public void Setup()
        {
            var mockPasswordHelper = new Mock<IPasswordHelper>();
            mockPasswordHelper.Setup(x => x.VerifyPasswordHash("correct_password", "correct_hash", "correct_salt")).Returns(true);

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetUserByUsernameAsync("juan_perez")).ReturnsAsync(new User()
            {
                Id = Guid.NewGuid(),
                Name = "Juan Perez",
                Username = "juan_perez",
                PasswordHash = "correct_hash",
                PasswordSalt = "correct_salt",
                RoleId = Guid.NewGuid(), 
                RoleName = "Administradoe"
            });

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetSection("Jwt")["Secret"]).Returns(default(Guid).ToString());
            mockConfiguration.Setup(x => x.GetSection("Jwt")["ExpirationInMinutes"]).Returns("60");

            _securityBL = new SecurityBL(mockUserRepository.Object, mockPasswordHelper.Object, mockConfiguration.Object);
        }

        [Test]
        public async Task Authenticate_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            var loginRequest = new LoginRequest() { Username = "juan_perez", Password = "correct_password" };

            // Act
            var result = await _securityBL.AuthenticateAsync(loginRequest);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Authenticate_IncorrectPassword_ReturnsFalse()
        {
            // Arrange
            var loginRequest = new LoginRequest() { Username = "juan_perez", Password = "incorrect_password" };

            // Act
            var result = await _securityBL.AuthenticateAsync(loginRequest);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Authenticate_UserNotFound_ReturnsFalse()
        {
            // Arrange
            var loginRequest = new LoginRequest() { Username = "nonexistent_username", Password = "incorrect_password" };

            // Act
            var result = await _securityBL.AuthenticateAsync(loginRequest);

            // Assert
            Assert.IsNull(result);
        }
    }
}
