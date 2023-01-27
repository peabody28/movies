using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;

namespace movies.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private FilmDbContext FilmDbContext { get; set; }

        public CountryRepository(FilmDbContext filmDbContext)
        {
            FilmDbContext = filmDbContext;
        }

        public ICountry Object(string code)
        {
            return FilmDbContext.Country.FirstOrDefault(c => c.Code.Equals(code));
        }
    }
}
