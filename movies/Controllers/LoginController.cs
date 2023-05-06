using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Attributes;
using movies.Interfaces.Operations;
using movies.Models.Login;

namespace movies.Controllers
{
    public class LoginController : BaseController
    {
        #region [ Dependency -> Operations ]

        [Dependency]
        public IIdentityOperation IdentityOperation { get; set; }

        [Dependency]
        public IAuthorizationOperation AuthorizationOperation { get; set; }

        #endregion

        public LoginController(DependencyFactory dependencyFactory) 
        {
            dependencyFactory.ResolveDependency(this);
        }

        [HttpPost]
        [AllowAnonymous]
        public TokenModel Authorize(UserAuthorizeModel model)
        {
            var identity = IdentityOperation.Object(model.NickName, model.Password);
            if (identity == null)
                throw new BadHttpRequestException("Cannot find a user with specified credentials"); // TODO: add validation

            var token = AuthorizationOperation.GenerateToken(identity);

            return new TokenModel
            {
                AccessToken = token.AccessToken,
                ExpirationDate = token.ExpirationDate
            };
        }
    }
}
