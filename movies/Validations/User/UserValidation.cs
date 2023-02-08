using movies.Attributes;
using movies.Constants;
using movies.Interfaces.Repositories;
using movies.Validators;

namespace movies.Validations.User
{
    public class UserValidation
    {
        [Dependency]
        public IUserRepository UserRepository { get; set; }

        public UserValidation(DependencyFactory dependencyFactory)
        {
            dependencyFactory.ResolveDependency(this);
        }

        public ValidationResult ValidateName(string? nickName)
        {
            if (string.IsNullOrWhiteSpace(nickName))
                return new ValidationResult(ValidationApiErrorConstants.USER_NAME_REQUIRED, "User name is required");

            return ValidationResult.Empty();
        }

        public ValidationResult CheckDuplicates(string nickName)
        {
            var user = UserRepository.Object(nickName);
            if (user != null)
                return new ValidationResult(ValidationApiErrorConstants.USER_ALREADY_EXISTS, "User with specified nickname already exists");

            return ValidationResult.Empty();
        }
    }
}
