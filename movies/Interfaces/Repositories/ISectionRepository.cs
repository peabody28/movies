using movies.Interfaces.Entities;

namespace movies.Interfaces.Repositories
{
    public interface ISectionRepository
    {
        ISection? Object(string name);
        IEnumerable<ISection> Collection();
    }
}
