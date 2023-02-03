using movies.Attributes;
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

        public ValidationResult CheckDuplicates(string nickName)
        {
            var user = UserRepository.Object(nickName);
            if (user != null)
                return new ValidationResult("USER_ALREADY_EXISTS", "User with specified nickname already exists");

            return ValidationResult.Empty();
        }
    }
}
