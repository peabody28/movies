using Microsoft.AspNetCore.Mvc;
using movies.Attributes;
using movies.Interfaces.Entities;
using movies.Interfaces.Operations;

namespace movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController : ControllerBase
    {
        [Dependency]
        public IUserOperation UserOperation { get; set; }

        public IUser CurrentUser { 
            get
            {
                return UserOperation.CurrentUser!;
            } 
        }
    }
}
