using Moq;
using StoreApp.Interface;
using StoreApp.Models;
using StoreApp.Services.Auth;
using StoreApp.Tests.Data;

namespace StoreApp.Tests.Auth
{
    public class AuthServiceTests
    {
        [Fact]
        public void AuthenticateUser_Should_ReturnSuccess_WhenCredentials_Are_Correct()
        {
            var mockData = new Mock<IDataManager>();

            var users = new List<UserAccount>
            {

                new UserAccount
                {
                    Username = "test",
                    PasswordHash = AuthService.GetMd5Hash("1234"),
                    Role = "customer",
                    IsBanned = false
                }
            };

            mockData.Setup(m => m.LoadUsers()).Returns(users);

            var authService = new AuthService(mockData.Object);

            var result = authService.AuthenticateUser("test", "1234");

            Assert.True(result.Succes);
            Assert.NotNull(result.UserMenu);
        }
        [Fact]
        public void AuthenticateUser_Should_Fail_WrongPassword()
        {
            var mockData = new Mock<IDataManager>();

            var users = new List<UserAccount>
            { new UserAccount
            {
                Username = "test",
                PasswordHash = AuthService.GetMd5Hash("constanta"), // true pass
                Role = "customer",
                IsBanned = false
            }
            };

            mockData.Setup(m => m.LoadUsers()).Returns(users);

            var authService = new AuthService(mockData.Object);

            var result = authService.AuthenticateUser("test", "wrong-constanta"); // fail pass

            Assert.False(result.Succes);
            Assert.Null(result.UserMenu);
        }

        [Fact]
        public void AuthenticateUser_Should_Fail_User_Is_Banned()
        {
            var mockData = new Mock<IDataManager>();

            var users = new List<UserAccount>
            {

                new UserAccount
                {
                    Username = "test",
                    PasswordHash = AuthService.GetMd5Hash("1111"),
                    Role = "customer",
                    IsBanned = true
                }
            };

            mockData.Setup(m => m.LoadUsers()).Returns(users);

            var authService = new AuthService(mockData.Object);

            var result = authService.AuthenticateUser("test", "1111");

            Assert.False(result.Succes);
            Assert.True(result.IsBanned);
            Assert.Null(result.UserMenu);
        }

        [Fact]
        public void Authenticate_Should_Return_User_When_Credentials_Are_Correct()
        {
            var dataManager = new FakeDataManager();
            var auth = new AuthService(dataManager);

            var result = auth.AuthenticateUser("test", "1234");

            Assert.NotNull(result);
            Assert.Equal("test", result.User.Username);

        }

    }
}