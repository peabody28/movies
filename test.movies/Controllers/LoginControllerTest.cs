using Microsoft.AspNetCore.Http;
using movies.Controllers;
using movies.Models.Login;
using NUnit.Framework;
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
        public void Authorize([Values(null, "Test")] string nickname)
        {
            // Arrange
            DependencyFactoryMock dfm = new DependencyFactoryMock();
            var controller = new LoginController(dfm);

            var model = new UserAuthorizeModel
            {
                NickName = nickname,
                Password = "1234",
            };

            // Act
            var method = controller.Authorize;

            // Assert
            TokenModel resp = null;
            if (string.IsNullOrWhiteSpace(nickname))
                Assert.Throws<BadHttpRequestException>(() => method(model));
            else
                resp = method.Invoke(model);

            if (!string.IsNullOrWhiteSpace(nickname))
            {
                Assert.NotNull(resp);
                Assert.IsNotNull(resp.AccessToken);
                Assert.IsNotEmpty(resp.AccessToken);
            }
        }
    }
}
