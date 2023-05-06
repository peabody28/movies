using movies.Attributes;
using movies.Helpers;
using movies.Interfaces.Operations;
using movies.Interfaces.Repositories;
using System.Security.Claims;

namespace movies.Operations
{
    public class IdentityOperation : IIdentityOperation
    {
        [Dependency]
        public IUserRepository UserRepository { get; set; }

        public IdentityOperation(DependencyFactory dependencyFactory)
        {
            dependencyFactory.ResolveDependency(this);
        }

        public ClaimsIdentity? Object(string nickName, string password)
        {
            var passwordHash = MD5Helper.Hash(password);

            var user = UserRepository.Object(nickName, passwordHash);
            if (user == null)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.NickName)
            };

            return new ClaimsIdentity(claims);
        }
    }
}
