using Microsoft.AspNetCore.Mvc;
using movies.Interfaces.Entities;
using movies.Interfaces.Operations;

namespace movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        public IUserOperation UserOperation { get; set; }

        public BaseController(IUserOperation userOperation)
        { 
            UserOperation = userOperation;
        }

        public IUser CurrentUser { 
            get
            {
                return UserOperation.CurrentUser!;
            } 
        }
    }
}
