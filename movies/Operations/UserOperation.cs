using Azure.Core;
using Microsoft.AspNetCore.Http;
using movies.Interfaces.Entities;
using movies.Interfaces.Operations;
using movies.Interfaces.Repositories;
using movies.Repositories;

namespace movies.Operations
{
    public class UserOperation : IUserOperation
    {
        private readonly IHttpContextAccessor HttpContextAccessor;

        public IUserRepository UserRepository { get; set; }

        public UserOperation(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            HttpContextAccessor = httpContextAccessor;
            UserRepository = userRepository;
        }

        public IUser CurrentUser
        {
            get
            {
                var nickName = HttpContextAccessor.HttpContext?.User?.Identity?.Name;
                if (string.IsNullOrWhiteSpace(nickName))
                    return null;

                return UserRepository.Object(nickName);
            }
        }
    }
}
