using movies.Interfaces.Repositories;
using movies.Validators;

namespace movies.Validations.User
{
    public class UserValidation
    {
        public IUserRepository UserRepository { get; set; }

        public UserValidation(IUserRepository userRepository) 
        {
            UserRepository = userRepository;
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
