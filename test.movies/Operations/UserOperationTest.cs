using Microsoft.AspNetCore.Http;
using Moq;
using movies.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using test.movies.Mocks;

namespace test.movies.Operations
{
    internal class UserOperationTest
    {
        private IHttpContextAccessor HttpContextAccessorMock(string nickname)
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(m => m.HttpContext).Returns(new DefaultHttpContext());
            mockHttpContextAccessor.Setup(m => m.HttpContext.User).Returns(new ClaimsPrincipal());
            mockHttpContextAccessor.Setup(m => m.HttpContext.User.Identity).Returns(new ClaimsIdentity());
            mockHttpContextAccessor.Setup(m => m.HttpContext.User.Identity.Name).Returns(nickname);

            return mockHttpContextAccessor.Object;
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Object([Values(null, "Test")] string nickname)
        {
            // Arrange
            DependencyFactoryMock dfm = new DependencyFactoryMock();
            var userOperation = new UserOperation(dfm, HttpContextAccessorMock(nickname));

            // Act
            var resp = userOperation.CurrentUser;

            // Assert
            if (string.IsNullOrWhiteSpace(nickname))
                Assert.IsNull(resp);
            else
                Assert.IsNotNull(resp);
        }
    }
}
