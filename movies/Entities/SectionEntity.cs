using movies.Interfaces.Entities;

namespace movies.Entities
{
    public class SectionEntity : ISection
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
