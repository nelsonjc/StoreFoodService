using AutoMapper;
using Moq;
using ShopFood.Application.Implements;
using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces.Application.Wrappers;
using ShopFood.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFood.Tests.Application.Implements
{
    [TestFixture]
    public class UserBLTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IPasswordHelper> _passwordHelperMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHelperMock = new Mock<IPasswordHelper>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserRequest, User>();
            });
            _mapper = mapperConfig.CreateMapper();
            _passwordHelperMock.Setup(x => x.CreatePasswordHash("password123")).Returns("salt:hashed_password");
        }

        [Test]
        public async Task Test_DeleteAsync_Should_Delete_User()
        {
            // Arrange
            var userBL = new UserBL(_mapper, _userRepositoryMock.Object, _passwordHelperMock.Object);
            var userId = Guid.NewGuid();

            // Act
            await userBL.DeleteAsync(userId);

            // Assert
            _userRepositoryMock.Verify(x => x.DeleteAsync(userId), Times.Once);
        }

        [Test]
        public async Task Test_GetAllAsync_Should_Return_List()
        {
            // Arrange
            var userBL = new UserBL(_mapper, _userRepositoryMock.Object, _passwordHelperMock.Object);
            var userList = new List<User> { new User(), new User() };
            _userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(userList);

            // Act
            var result = await userBL.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userList.Count, result.Count());
        }

        [Test]
        public async Task Test_GetByIdAsync_Should_Return_UserDto()
        {
            // Arrange
            var userBL = new UserBL(_mapper, _userRepositoryMock.Object, _passwordHelperMock.Object);
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Name = "Juan Perez" };
            var expectedUserDto = new UserDto { Id = userId, Name = "Juan Perez" };
            _userRepositoryMock.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await userBL.GetByIdAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUserDto.Id, result.Id);
            Assert.AreEqual(expectedUserDto.Name, result.Name);
        }


        [Test]
        public async Task Test_InsertAsync_Should_Insert_User()
        {
            // Arrange
            var userBL = new UserBL(_mapper, _userRepositoryMock.Object, _passwordHelperMock.Object);
            var userRequest = new UserRequest
            {
                Name = "Jane Doe",
                Username = "janedoe",
                Password = "password123",
                RoleId = Guid.NewGuid()
            };
            var user = _mapper.Map<User>(userRequest);

            // Act
            await userBL.InsertAsync(userRequest);

            // Assert
            _userRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<User>())).Callback<User>(insertedUser =>
            {
                Assert.AreEqual(user.Id, insertedUser.Id, "Inserted user Id should match.");
            });
        }



        [Test]
        public async Task Test_InsertCustomerAsync_Should_Insert_Customer_With_Customer_Role()
        {
            // Arrange
            var userBL = new UserBL(_mapper, _userRepositoryMock.Object, _passwordHelperMock.Object);
            var userRequest = new UserRequest
            {
                Name = "Jane Doe",
                Username = "janedoe",
                Password = "password123"
            };
            var user = _mapper.Map<User>(userRequest);

            // Act
            await userBL.InsertCustomerAsync(userRequest);

            // Assert
            _userRepositoryMock.Setup(x => x.InsertCustomerAsync(It.IsAny<User>())).Callback<User>(insertedCustomer =>
            {
                Assert.AreEqual(user.Id, insertedCustomer.Id, "Inserted customer Id should match.");
            });
        }


        [Test]
        public async Task Test_UpdateAsync_Should_Update_User()
        {
            // Arrange
            var userBL = new UserBL(_mapper, _userRepositoryMock.Object, _passwordHelperMock.Object);
            var userRequest = new UserRequest
            {
                Id = Guid.NewGuid(),
                Name = "Jane Doe",
                Username = "janedoe",
                Password = "password123",
                RoleId = Guid.NewGuid(),
                Active = true
            };
            var user = _mapper.Map<User>(userRequest);

            // Act
            await userBL.UpdateAsync(userRequest);

            // Assert
            _userRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<User>())).Callback<User>(updatedUser =>
            {
                Assert.AreEqual(user.Id, updatedUser.Id, "Updated user Id should match.");
            });
        }
    }
}
