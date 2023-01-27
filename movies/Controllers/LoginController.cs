using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Interfaces.Operations;
using movies.Models.Login;

namespace movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        public IIdentityOperation IdentityOperation { get; set; }

        public IAuthorizationOperation AuthorizationOperation { get; set; }

        public LoginController(IIdentityOperation identityOperation, IAuthorizationOperation authorizationOperation)
        {
            IdentityOperation = identityOperation;
            AuthorizationOperation = authorizationOperation;
        }

        [HttpPost]
        [AllowAnonymous]
        public TokenModel Authorize(UserAuthorizeModel model)
        {
            var identity = IdentityOperation.Object(model.NickName, model.Password);
            if (identity == null)
                return null;

            var token = AuthorizationOperation.GenerateToken(identity);

            return new TokenModel
            {
                AccessToken = token.AccessToken,
                ExpirationDate = token.ExpirationDate
            };
        }
    }
}
