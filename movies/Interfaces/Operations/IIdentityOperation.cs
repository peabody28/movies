using System.Security.Claims;

namespace movies.Interfaces.Operations
{
    public interface IIdentityOperation
    {
        ClaimsIdentity Object(string nickName, string password);
    }
}
