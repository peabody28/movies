using movies.Constants;
using movies.Validations.User;
using movies.Validators;
using test.movies.Constants;
using test.movies.Mocks;

namespace test.movies.Validations
{
    public class UserValidationTest
    {
        private DependencyFactoryMock DependencyFactoryMock { get; set; }
        
        private UserValidation UserValidation { get; set; }


        [SetUp]
        public void Setup()
        {
            DependencyFactoryMock = new DependencyFactoryMock();
            UserValidation = new UserValidation(DependencyFactoryMock);
        }

        [Test]
        public void CheckDuplicates([Values("undefined_some_name", TestDataConstants.ExistingUserName)] string? userName)
        {
            // Arrange

            // Act
            var resp = UserValidation.CheckDuplicates(userName!);

            // Assert
            Assert.IsNotNull(resp);
            Assert.IsInstanceOf<ValidationResult>(resp);

            if (userName.Equals(TestDataConstants.ExistingUserName))
            {
                Assert.IsTrue(resp.HasErrors);
                Assert.Contains(ValidationApiErrorConstants.USER_ALREADY_EXISTS, resp.Errors.Select(item => item.Code).ToList());
            }
            else
                Assert.IsTrue(resp.IsValid);
        }

        [Test]
        public void ValidateName([Values("undefined_some_name", TestDataConstants.ExistingUserName)] string? userName)
        {
            // Arrange
           
            // Act
            var resp = UserValidation.ValidateName(userName);

            // Assert
            Assert.IsNotNull(resp);
            Assert.IsInstanceOf<ValidationResult>(resp);

            if (string.IsNullOrWhiteSpace(userName))
            {
                Assert.IsTrue(resp.HasErrors);
                Assert.Contains(ValidationApiErrorConstants.USER_NAME_REQUIRED, resp.Errors.Select(item => item.Code).ToList());
            }
            else
                Assert.IsTrue(resp.IsValid);
        }
    }
}
