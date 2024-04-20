using Moq;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces.Repository;

namespace ShopFood.Tests.Infraestructura
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private Mock<IUserRepository> _userRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Test]
        public async Task Test_GetAllAsync_Should_Return_NonEmpty_List()
        {
            // Arrange
            var userRepository = _userRepositoryMock.Object;
            _userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<User> { new User(), new User() });

            // Act
            var userList = await userRepository.GetAllAsync();

            // Assert
            Assert.NotNull(userList);
            Assert.IsNotEmpty(userList);
        }

        [Test]
        public async Task Test_GetByIdAsync_Should_Return_User()
        {
            // Arrange
            var userRepository = _userRepositoryMock.Object;
            var userId = Guid.NewGuid();
            var expectedUser = new User { Id = userId, Name = "Juan Perez", Username = "juanperez" };
            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(expectedUser);

            // Act
            var user = await userRepository.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(user);
            Assert.AreEqual(expectedUser, user);
        }

        [Test]
        public async Task Test_InsertAsync_Should_Create_User()
        {
            // Arrange
            var userRepository = _userRepositoryMock.Object;
            var newUser = new User { Name = "Juan Perez", Username = "juanperez", RoleId = Guid.NewGuid() };
            _userRepositoryMock.Setup(x => x.InsertAsync(newUser)).Returns(Task.CompletedTask);

            // Act
            await userRepository.InsertAsync(newUser);

            // Assert
            _userRepositoryMock.Verify(x => x.InsertAsync(newUser), Times.Once);
        }

        [Test]
        public async Task Test_DeleteAsync_Should_Delete_User()
        {
            // Arrange
            var userRepository = _userRepositoryMock.Object;
            var userId = Guid.NewGuid();

            // Act
            await userRepository.DeleteAsync(userId);

            // Assert
            _userRepositoryMock.Verify(x => x.DeleteAsync(userId), Times.Once);
        }

        [Test]
        public async Task Test_GetUserByUsernameAsync_Should_Return_User()
        {
            // Arrange
            var userRepository = _userRepositoryMock.Object;
            var username = "juanperez";
            var expectedUser = new User { Id = Guid.NewGuid(), Name = "JJuan Perez", Username = username };
            _userRepositoryMock.Setup(x => x.GetUserByUsernameAsync(username)).ReturnsAsync(expectedUser);

            // Act
            var user = await userRepository.GetUserByUsernameAsync(username);

            // Assert
            Assert.NotNull(user);
            Assert.AreEqual(expectedUser, user);
        }

        [Test]
        public async Task Test_InsertCustomerAsync_Should_Create_Customer()
        {
            // Arrange
            var userRepository = _userRepositoryMock.Object;
            var newCustomer = new User { Name = "Juan Perez", Username = "juanperez", RoleId = Guid.NewGuid() };
            _userRepositoryMock.Setup(x => x.InsertCustomerAsync(newCustomer)).Returns(Task.CompletedTask);

            // Act
            await userRepository.InsertCustomerAsync(newCustomer);

            // Assert
            _userRepositoryMock.Verify(x => x.InsertCustomerAsync(newCustomer), Times.Once);
        }
    }
}
