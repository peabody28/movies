using movies.Attributes;
using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;

namespace movies.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        [Dependency]
        public FilmDbContext FilmDbContext { get; set; }

        public SectionRepository(DependencyFactory dependencyFactory)
        {
            dependencyFactory.ResolveDependency(this);
        }

        public IEnumerable<ISection> Collection()
        {
            return FilmDbContext.Section.ToList();
        }

        public ISection? Object(string name)
        {
            return FilmDbContext.Section.FirstOrDefault(section => section.Name.Equals(name));
        }
    }
}
