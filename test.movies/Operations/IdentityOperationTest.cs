using movies.Controllers;
using movies.Interfaces.Entities;
using movies.Models.Login;
using movies.Operations;
using NUnit.Framework;
using System.Security.Claims;
using test.movies.Mocks;

namespace test.movies.Operations
{
    internal class IdentityOperationTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Object([Values(null,  "Test")] string nickname)
        {
            // Arrange
            DependencyFactoryMock dfm = new DependencyFactoryMock();
            var identityOperation = new IdentityOperation(dfm);

            // Act
            var resp = identityOperation.Object(nickname, "some_password");

            // Assert
            if(string.IsNullOrWhiteSpace(nickname))
                Assert.IsNull(resp);
            else
            {
                Assert.IsNotNull(resp);
                Assert.That(resp.Claims, Is.Not.Empty);
                Assert.That(resp.Name, Is.Not.Null);
            }
        }
    }
}
