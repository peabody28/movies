using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Interfaces.Operations;
using movies.Interfaces.Repositories;
using movies.Models.Login;

namespace movies.Controllers
{
    public class LoginController : BaseController
    {
        #region [ Dependency -> Operations ]

        public IIdentityOperation IdentityOperation { get; set; }

        public IAuthorizationOperation AuthorizationOperation { get; set; }

        #endregion

        public LoginController(IUserRepository userRepository, IIdentityOperation identityOperation, IAuthorizationOperation authorizationOperation) : base(userRepository)
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
