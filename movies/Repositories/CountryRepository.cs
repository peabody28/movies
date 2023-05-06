using movies.Attributes;
using movies.Entities;
using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;

namespace movies.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        [Dependency]
        public FilmDbContext FilmDbContext { get; set; }

        [Dependency]
        public IServiceProvider ServiceProvider { get; set; }

        public CountryRepository(DependencyFactory dependencyFactory)
        {
            dependencyFactory.ResolveDependency(this);
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

        public ICountry? Object(string code)
        {
            return FilmDbContext.Country.FirstOrDefault(c => c.Code.Equals(code));
        }
    }
}
