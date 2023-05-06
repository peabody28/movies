using FluentValidation;
using movies.Models.Film;
using movies.Validations.Film;

namespace movies.Validators.Film
{
    public class FilmCreateValidator : AbstractValidator<FilmCreateModel>
    {
        public FilmCreateValidator(FilmValidation filmValidation) 
        {
            RuleFor(model => model)
                .Custom((model, context) => context.AddFailures(nameof(model.Title), filmValidation.ValidateTitle(model.Title)))
                .Custom((model, context) => context.AddFailures(nameof(model.Title), filmValidation.CheckDuplicates(model.Title!)))
                .Custom((model, context) => context.AddFailures(nameof(model.RatingTypeName), filmValidation.ValidateRatingTypeName(model.RatingTypeName)));
        }
    }
}
