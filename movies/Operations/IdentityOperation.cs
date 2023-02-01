using movies.Helpers;
using movies.Interfaces.Operations;
using movies.Interfaces.Repositories;
using System.Security.Claims;

namespace movies.Operations
{
    public class IdentityOperation : IIdentityOperation
    {
        public IUserRepository UserRepository { get; set; }

        public IdentityOperation(IUserRepository userRepository)
        {
            UserRepository = userRepository;
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
