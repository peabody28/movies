using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Helpers;
using movies.Interfaces.Repositories;
using movies.Models.Registration;

namespace movies.Controllers
{
    public class RegistrationController : BaseController
    {
        public RegistrationController(IUserRepository userRepository) : base(userRepository) { }

        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Registrate(UserRegistrateModel model)
        {
            var passwordHash = MD5Helper.Hash(model.Password);

            UserRepository.Create(model.NickName, model.Email, passwordHash, model.FirstName, model.LastName);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
