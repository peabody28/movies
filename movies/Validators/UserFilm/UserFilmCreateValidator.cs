using FluentValidation;
using movies.Interfaces.Operations;
using movies.Models.UserFilm;
using movies.Validations.Film;
using movies.Validations.Section;

namespace movies.Validators.UserFilm
{
    public class UserFilmCreateValidator : AbstractValidator<UserFilmCreateModel>
    {
        public UserFilmCreateValidator(FilmValidation filmValidation, SectionValidation sectionValidation, IUserOperation userOperation)
        {
            RuleFor(model => model)
                .Custom((model, context) => context.AddFailures(nameof(model.FilmId), filmValidation.ValidateFilmId(model.FilmId)))
                .Custom((model, context) => context.AddFailures(nameof(model.SectionName), sectionValidation.Validate(model.SectionName)))
                .Custom((model, context) => context.AddFailures(string.Empty, filmValidation.CheckDuplicates(userOperation.CurrentUser!, model.FilmId, model.SectionName)));
        }
    }
}
