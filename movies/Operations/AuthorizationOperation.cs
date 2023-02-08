using Microsoft.IdentityModel.Tokens;
using movies.Attributes;
using movies.Interfaces.Operations;
using movies.Models.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace movies.Operations
{
    public class AuthorizationOperation : IAuthorizationOperation
    {
        [Dependency]
        public IConfiguration Configuration { get; set; }

        public AuthorizationOperation(DependencyFactory dependencyFactory)
        {
            dependencyFactory.ResolveDependency(this);
        }

        public TokenDtoModel GenerateToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var issuer = Configuration.GetSection("AuthOptions:ISSUER").Value;
            var audience = Configuration.GetSection("AuthOptions:AUDIENCE").Value;
            var lifetime = Configuration.GetSection("AuthOptions:LIFETIME").Get<double>();
            var key = Configuration.GetSection("AuthOptions:KEY").Value;
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!));

            var expirationDate = now.Add(TimeSpan.FromMinutes(lifetime));
            var jwt = new JwtSecurityToken(issuer, audience, identity.Claims, now,
                expirationDate, new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenDtoModel
            {
                AccessToken = token,
                ExpirationDate = expirationDate
            };
        }
    }
}
