﻿using FluentValidation;
using FluentValidation.Results;
using movies.Models.Registration;
using movies.Validations.User;

namespace movies.Validators.Registration
{
    public class UserRegistrateValidator : AbstractValidator<UserRegistrateModel>
    {
        public UserRegistrateValidator(UserValidation userValidation)
        {
            RuleFor(model => model)
                .Custom((model, context) => context.AddFailures(nameof(model.NickName), userValidation.ValidateName(model.NickName)))
                .Custom((model, context) => context.AddFailures(nameof(model.NickName), userValidation.CheckDuplicates(model.NickName)));
        }
    }
}
