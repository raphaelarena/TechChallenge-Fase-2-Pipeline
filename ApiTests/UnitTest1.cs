using Moq;
using WebApplication1.DTO;
using WebApplication1.Entity;
using WebApplication1.Enums;
using WebApplication1.Interface;
using WebApplication1.Repository;

namespace ApiTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestValidateLogin_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.GetUserByNameAndPassword("paloma.jesus", "12345"))
                          .Returns(new User {Name = "paloma.jesus"});

            // Assert e Act ao mesmo tempo
            Assert.NotNull(mockRepository.Object.GetUserByNameAndPassword("paloma.jesus", "12345"));
        }

        [Fact]
        public void ValidateUserName_Should_Return_Max()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();

            User user = new User
            {
                Name = "Raphael Arena",
                Password = "234571267",
                UserName = "raphaelarenagelonezi20101996raphaelarena_@hotmail.com.br",
                Id = Guid.NewGuid(),
                PermissionType = PermissionType.Admin,
            };

            mockRepository.Setup(repo => repo.Add(It.IsAny<User>()))
                 .Callback<User>((newUser) => user = newUser);

            // Act
            mockRepository.Object.Add(user);

            // Assert
            Assert.True(user.UserName.Length <= 50);
        }
    }
}