using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;

namespace movies.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private FilmDbContext FilmDbContext { get; set; }

        public SectionRepository(FilmDbContext filmDbContext)
        {
            FilmDbContext = filmDbContext;
        }

        public IEnumerable<ISection> Collection()
        {
            return FilmDbContext.Section;
        }

        public ISection Object(string name)
        {
            return FilmDbContext.Section.FirstOrDefault(section => section.Name.Equals(name));
        }


    }
}
