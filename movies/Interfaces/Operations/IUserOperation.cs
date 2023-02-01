using movies.Interfaces.Entities;

namespace movies.Interfaces.Operations
{
    public interface IUserOperation
    {
        public IUser CurrentUser { get; }
    }
}
