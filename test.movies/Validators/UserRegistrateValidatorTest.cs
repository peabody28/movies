using FluentValidation.TestHelper;
using movies.Constants;
using movies.Models.Registration;
using movies.Validations.User;
using movies.Validators.Registration;
using test.movies.Constants;
using test.movies.Mocks;

namespace test.movies.Validators
{
    public class UserRegistrateValidatorTest
    {
        private UserRegistrateValidator UserRegistrateValidator { get; set; }

        [SetUp]
        public void Setup()
        {
            var dfm = new DependencyFactoryMock();
            var valdiation = new UserValidation(dfm);
            UserRegistrateValidator = new UserRegistrateValidator(valdiation);
        }

        [Test]
        public void Validate([Values(null, "", " ", "some_name", TestDataConstants.ExistingUserName)] string nickname)
        {
            // Arange
            var userRegistrateModel = new UserRegistrateModel
            {
                NickName = nickname,
            };

            // Act
            var result = UserRegistrateValidator.TestValidate(userRegistrateModel);

            // Assert
            if(string.IsNullOrWhiteSpace(nickname))
                result.ShouldHaveValidationErrorFor(model => model.NickName)
                    .WithErrorCode(ValidationApiErrorConstants.USER_NAME_REQUIRED);
            else
            {
                if(nickname.Equals(TestDataConstants.ExistingUserName))
                    result.ShouldHaveValidationErrorFor(model => model.NickName)
                        .WithErrorCode(ValidationApiErrorConstants.USER_ALREADY_EXISTS);
                else
                    result.ShouldNotHaveValidationErrorFor(model => model.NickName);
            }
        }
    }
}
