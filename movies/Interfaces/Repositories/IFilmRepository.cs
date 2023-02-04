using movies.Interfaces.Entities;

namespace movies.Interfaces.Repositories
{
    public interface IFilmRepository
    {
        IFilm? Object(Guid id);

        IFilm? Object(string title);

        void Update(IFilm film);

        IEnumerable<IFilm> Collection();

        IEnumerable<IFilm> Collection(int pageSize, int pageNumber);

        /// <summary>
        /// Find a rows that contains text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        IEnumerable<IFilm> Collection(string text);

        int Count();

        IFilm? Create(IDirector director, IRatingType ratingType, decimal ratingValue, ICountry country, string title, string description, int? year = null);

    }
}
