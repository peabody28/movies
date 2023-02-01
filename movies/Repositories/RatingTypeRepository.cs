using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;

namespace movies.Repositories
{
    public class RatingTypeRepository : IRatingTypeRepository
    {
        private FilmDbContext FilmDbContext { get; set; }

        public RatingTypeRepository(FilmDbContext filmDbContext)
        {
            FilmDbContext = filmDbContext;
        }

        public IRatingType? Object(string name)
        {
            return FilmDbContext.RatingType.FirstOrDefault(r => r.Name.Equals(name));
        }
    }
}
