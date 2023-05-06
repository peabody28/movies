using movies.Interfaces.Entities;

namespace movies.Interfaces.Repositories
{
    public interface ICountryRepository
    {
        ICountry? Object(string code);

        ICountry Create(string code, string? name = null);
    }
}
