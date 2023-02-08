using movies.Attributes;
using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;

namespace movies.Repositories
{
    public class RatingTypeRepository : IRatingTypeRepository
    {
        [Dependency]
        public FilmDbContext FilmDbContext { get; set; }

        public RatingTypeRepository(DependencyFactory dependencyFactory)
        {
            dependencyFactory.ResolveDependency(this);
        }

        public IRatingType? Object(string name)
        {
            return FilmDbContext.RatingType.FirstOrDefault(r => r.Name.Equals(name));
        }
    }
}
