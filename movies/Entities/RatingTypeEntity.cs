using movies.Interfaces.Entities;

namespace movies.Entities
{
    public class RatingTypeEntity : IRatingType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
