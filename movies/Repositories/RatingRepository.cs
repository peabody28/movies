using Microsoft.EntityFrameworkCore;
using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;

namespace movies.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private FilmDbContext FilmDbContext { get; set; }

        public RatingRepository(FilmDbContext filmDbContext)
        {
            FilmDbContext = filmDbContext;
        }

        public IRating? Object(IFilm film, IRatingType ratingType)
        {
            return FilmDbContext.Rating.Include(r => r.Film).Include(r => r.RatingType).FirstOrDefault(r => r.Film.Equals(film) && r.RatingType.Equals(ratingType));
        }

        public IEnumerable<IRating> Collection(IFilm film)
        {
            return FilmDbContext.Rating.Include(r => r.Film).Include(r => r.RatingType).Where(r => r.Film.Equals(film)).ToList();
        }
    }
}
