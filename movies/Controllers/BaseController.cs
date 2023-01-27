using Microsoft.AspNetCore.Mvc;
using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;

namespace movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        public IUserRepository UserRepository { get; set; }

        public BaseController(IUserRepository userRepository) 
        {
            UserRepository = userRepository;
        }

        public IUser CurrentUser { 
            get
            {
                var nickName = Request.HttpContext?.User?.Identity?.Name;
                if (string.IsNullOrWhiteSpace(nickName))
                    return null;

                return UserRepository.Object(nickName);
            } 
        }
    }
}
