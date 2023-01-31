using movies.Entities;
using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;

namespace movies.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private FilmDbContext FilmDbContext { get; set; }
        private IServiceProvider ServiceProvider { get; set; }

        public CountryRepository(FilmDbContext filmDbContext, IServiceProvider serviceProvider)
        {
            FilmDbContext = filmDbContext;
            ServiceProvider = serviceProvider;
        }

        public ICountry Create(string code, string? name = null)
        {
            var countryEntity = ServiceProvider.GetRequiredService<ICountry>();
            countryEntity.Id = Guid.NewGuid();
            countryEntity.Name = name ?? code;
            countryEntity.Code = code;

            var country = FilmDbContext.Country.Add(countryEntity as CountryEntity);

            FilmDbContext.SaveChanges();

            return country.Entity;
        }

        public ICountry Object(string code)
        {
            return FilmDbContext.Country.FirstOrDefault(c => c.Code.Equals(code));
        }
    }
}
