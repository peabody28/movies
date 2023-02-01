using FluentValidation;
using movies.Interfaces.Operations;
using movies.Models.UserFilm;
using movies.Validations.Film;
using movies.Validations.Section;

namespace movies.Validators.UserFilm
{
    public class UserFilmDeleteValidator : AbstractValidator<UserFilmDeleteModel>
    {
        public UserFilmDeleteValidator(FilmValidation filmValidation)
        {
            RuleFor(model => model)
                .Custom((model, context) => context.AddFailures(nameof(model.Id), filmValidation.ValidateUserFilmId(model.Id)));
        }
    }
}
