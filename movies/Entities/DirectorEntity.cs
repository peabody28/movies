using movies.Interfaces.Entities;

namespace movies.Entities
{
    public class DirectorEntity : IDirector
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public int? Age { get; set; }
    }
}
