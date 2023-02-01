﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Helpers;
using movies.Interfaces.Operations;
using movies.Interfaces.Repositories;
using movies.Models.Registration;

namespace movies.Controllers
{
    public class RegistrationController : BaseController
    {
        public IUserRepository UserRepository { get; set; }

        public RegistrationController(IUserRepository userRepository, IUserOperation userOperation) : base(userOperation) 
        {
            UserRepository = userRepository;
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
