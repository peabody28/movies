using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;

namespace movies.Repositories
{
    public class DirectorRepository : IDirectorRepository
    {
        private FilmDbContext FilmDbContext { get; set; }

        public DirectorRepository(FilmDbContext filmDbContext)
        {
            FilmDbContext = filmDbContext;
        }

        public IDirector Object(string firstName, string lastName)
        {
            return FilmDbContext.Director.FirstOrDefault(d => d.FirstName.Equals(firstName) && d.LastName.Equals(lastName));
        }
    }
}
