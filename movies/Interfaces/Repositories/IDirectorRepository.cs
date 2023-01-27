using movies.Interfaces.Entities;

namespace movies.Interfaces.Repositories
{
    public interface IDirectorRepository
    {
        IDirector Object(string firstName, string lastName);
    }
}
