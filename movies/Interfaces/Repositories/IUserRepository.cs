using movies.Interfaces.Entities;

namespace movies.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IUser Object(string nickName, string passwordHash);

        IUser Create(string nickName, string email, string passwordHash, string firstName = null, string lastName = null);
    }
}
