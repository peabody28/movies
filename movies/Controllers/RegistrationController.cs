using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Helpers;
using movies.Interfaces.Repositories;
using movies.Models.Login;
using movies.Models.Registration;

namespace movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        public IUserRepository UserRepository { get; set; }

        public RegistrationController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Registrate(UserRegistrateModel model)
        {
            var passwordHash = MD5Helper.Hash(model.Password);

            var user = UserRepository.Create(model.NickName, model.Email, passwordHash, model.FirstName, model.LastName);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
