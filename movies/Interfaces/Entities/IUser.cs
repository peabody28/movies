namespace movies.Interfaces.Entities
{
    public interface IUser
    {
        Guid Id { get; set; }

        string? FirstName { get; set; }

        string? LastName { get;set; }

        string NickName { get; set; }

        string Email { get; set; }

        string PasswordHash { get; set; }
    }
}
