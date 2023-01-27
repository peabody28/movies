using movies.Interfaces.Entities;

namespace movies.Entities
{
    public class UserEntity : IUser
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
