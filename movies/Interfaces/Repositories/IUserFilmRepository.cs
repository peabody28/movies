using movies.Interfaces.Entities;

namespace movies.Interfaces.Repositories
{
    public interface IUserFilmRepository
    {
        IUserFilm Create(IUser user, IFilm film, ISection? section = null);
        IEnumerable<IUserFilm> Collection(IUser user, ISection? section = null);
    }
}
