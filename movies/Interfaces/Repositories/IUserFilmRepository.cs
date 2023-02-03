using movies.Interfaces.Entities;

namespace movies.Interfaces.Repositories
{
    public interface IUserFilmRepository
    {
        IUserFilm Create(IUser user, IFilm film, ISection? section = null);

        IEnumerable<IUserFilm> Collection(IUser user, ISection? section = null, bool isDeleted = false);

        IEnumerable<IUserFilm> Collection(IUser user, int pageSize, int pageNumber, ISection? section = null, bool isDeleted = false);

        int Count(IUser user, ISection? section = null, bool isDeleted = false);

        IUserFilm? Object(Guid id, bool isDeleted = false);

        IUserFilm? Object(IUser user, IFilm film, ISection? section = null, bool isDeleted = false);

        void Delete(IUserFilm userFilm);
    }
}
