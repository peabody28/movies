using movies.Interfaces.Entities;

namespace movies.Interfaces.Repositories
{
    public interface IRatingRepository
    {
        IRating? Object(IFilm film, IRatingType ratingType);

        IEnumerable<IRating> Collection(IFilm film);
    }
}
