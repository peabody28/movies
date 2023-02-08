using movies.Helpers;
using movies.Operations;
using test.movies.Constants;
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
        public void Object([Values(null, TestDataConstants.ExistingUserName)] string nickname,
            [Values(null, TestDataConstants.ExistingUserPassword)] string password)
        {
            // Arrange
            DependencyFactoryMock dfm = new DependencyFactoryMock();
            var identityOperation = new IdentityOperation(dfm);
            var passwordHash = MD5Helper.Hash(password);

            // Act
            var resp = identityOperation.Object(nickname, password);

            // Assert
            if (nickname != null && passwordHash != null && nickname.Equals(dfm.ExistingUserStub.NickName) && passwordHash.Equals(dfm.ExistingUserStub.PasswordHash))
            {
                Assert.IsNotNull(resp);
                Assert.That(resp.Claims, Is.Not.Empty);
                Assert.That(resp.Name, Is.Not.Null);
            }
            else
                Assert.IsNull(resp);
        }
    }
}
