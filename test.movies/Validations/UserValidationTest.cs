using movies.Constants;
using movies.Validations.User;
using movies.Validators;
using test.movies.Constants;
using test.movies.Mocks;

namespace test.movies.Validations
{
    public class UserValidationTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CheckDuplicates([Values(null, "", " ", "undefined_some_name", TestDataConstants.ExistingUserName)] string? userName)
        {
            // Arrange
            DependencyFactoryMock dfm = new DependencyFactoryMock();
            var userValidation = new UserValidation(dfm);

            // Act
            var resp = userValidation.CheckDuplicates(userName);

            // Assert
            Assert.IsNotNull(resp);
            Assert.IsInstanceOf<ValidationResult>(resp);

            if (string.IsNullOrWhiteSpace(userName))
            {
                Assert.IsTrue(resp.HasErrors);
                Assert.Contains(ValidationApiErrorConstants.USER_NAME_REQUIRED, resp.Errors.Select(item => item.Code).ToList());
            }
            else
            {
                if (userName.Equals(TestDataConstants.ExistingUserName))
                {
                    Assert.IsTrue(resp.HasErrors);
                    Assert.Contains(ValidationApiErrorConstants.USER_ALREADY_EXISTS, resp.Errors.Select(item => item.Code).ToList());
                }
                else
                    Assert.IsTrue(resp.IsValid);
            }
        }
    }
}
