using ShopFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFood.Tests.Domain.Entities
{

    [TestFixture]
    public class UserTests
    {
        [Test]
        public void User_SetAndGetProperties_Success()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "Juan Perez";
            string username = "juan_perez";
            string passwordHash = "hashed_password";
            string passwordSalt = "salt_value";
            Guid roleId = Guid.NewGuid();
            string roleName = "Admin";


            // Act
            User user = new()
            {
                Id = id,
                Name = name,
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = roleId,
                RoleName = roleName
            };

            // Assert
            Assert.That(user.Id, Is.EqualTo(id));
            Assert.That(user.Username, Is.EqualTo(username));
            Assert.That(user.PasswordHash, Is.EqualTo(passwordHash));
            Assert.That(user.PasswordSalt, Is.EqualTo(passwordSalt));
            Assert.That(user.RoleId, Is.EqualTo(roleId));
            Assert.That(user.RoleName, Is.EqualTo(roleName));
        }

        [Test]
        public void User_DefaultConstructor_Success()
        {
            // Arrange & Act
            User user = new User();

            // Assert
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Id, Is.EqualTo(default(Guid)));
            Assert.That(user.RoleId, Is.EqualTo(default(Guid)));
            Assert.IsNull(user.Username);
            Assert.IsNull(user.PasswordHash);
            Assert.IsNull(user.PasswordSalt);
            Assert.IsNull(user.RoleName);
        }
    }
}
