using Microsoft.AspNetCore.Http;
using movies.Controllers;
using movies.Helpers;
using movies.Models.Login;
using NUnit.Framework;
using test.movies.Constants;
using test.movies.Mocks;

namespace test.movies.Controllers
{
    internal class LoginControllerTest
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Authorize([Values(null, TestDataConstants.ExistingUserName)] string nickname,
            [Values(null, "", TestDataConstants.ExistingUserPassword)] string password)
        {
            // Arrange
            DependencyFactoryMock dfm = new DependencyFactoryMock();
            var controller = new LoginController(dfm);

            var model = new UserAuthorizeModel
            {
                NickName = nickname,
                Password = password,
            };

            var passwordHash = MD5Helper.Hash(password);

            // Act
            var method = controller.Authorize;

            // Assert
            TokenModel resp = null;
            if (string.IsNullOrWhiteSpace(nickname) || string.IsNullOrWhiteSpace(password))
                Assert.Throws<BadHttpRequestException>(() => method(model));
            else
            {
                resp = method.Invoke(model);

                if (!string.IsNullOrWhiteSpace(nickname) && !string.IsNullOrWhiteSpace(passwordHash)
                    && nickname.Equals(dfm.ExistingUserStub.NickName) && passwordHash.Equals(dfm.ExistingUserStub.PasswordHash))
                {
                    Assert.NotNull(resp);
                    Assert.IsNotNull(resp.AccessToken);
                    Assert.IsNotEmpty(resp.AccessToken);
                }
            }
        }
    }
}
