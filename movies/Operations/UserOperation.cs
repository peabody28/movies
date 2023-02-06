using movies.Attributes;
using movies.Interfaces.Entities;
using movies.Interfaces.Operations;
using movies.Interfaces.Repositories;

namespace movies.Operations
{
    public class UserOperation : IUserOperation
    {
        private readonly IHttpContextAccessor HttpContextAccessor;

        [Dependency]
        public IUserRepository UserRepository { get; set; }

        public UserOperation(DependencyFactory dependencyFactory, IHttpContextAccessor httpContextAccessor)
        {
            dependencyFactory.ResolveDependency(this);
            HttpContextAccessor = httpContextAccessor;
        }

        public IUser? CurrentUser
        {
            get
            {
                var nickName = HttpContextAccessor.HttpContext?.User?.Identity?.Name;
                if (string.IsNullOrWhiteSpace(nickName))
                    return null;

                return UserRepository.Object(nickName);
            }
        }
    }
}
