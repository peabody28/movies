using movies.Models.Login;
using System.Security.Claims;

namespace movies.Interfaces.Operations
{
    public interface IAuthorizationOperation
    {
        TokenDtoModel GenerateToken(ClaimsIdentity identity);
    }
}
