using movies.Interfaces.Entities;

namespace movies.Interfaces.Repositories
{
    public interface IDirectorRepository
    {
        IDirector Create(string name, int? age = null);
        IDirector? Object(string name);
    }
}
