using movies.Interfaces.Entities;

namespace movies.Interfaces.Repositories
{
    public interface IRatingTypeRepository
    {
        IRatingType? Object(string name);
    }
}
