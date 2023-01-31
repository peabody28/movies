using movies.Interfaces.Entities;

namespace movies.Interfaces.Repositories
{
    public interface IDirectorRepository
    {
        IDirector Create(string firstName, string? lastName = null, int? age = null);
        IDirector Object(string firstName, string lastName);
    }
}
