using movies.Interfaces.Entities;

namespace movies.Entities
{
    public class CountryEntity : ICountry
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
