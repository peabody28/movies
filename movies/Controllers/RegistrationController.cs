using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Attributes;
using movies.Helpers;
using movies.Interfaces.Repositories;
using movies.Models.Registration;

namespace movies.Controllers
{
    public class RegistrationController : BaseController
    {
        [Dependency]
        public IUserRepository UserRepository { get; set; }

        public RegistrationController(DependencyFactory dependencyFactory) 
        {
            dependencyFactory.ResolveDependency(this);
        }

        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Registrate(UserRegistrateModel model)
        {
            var passwordHash = MD5Helper.Hash(model.Password);

            UserRepository.Create(model.NickName, model.Email, passwordHash);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
