using FluentValidation.TestHelper;
using movies.Constants;
using movies.Models.Film;
using movies.Validations.Film;
using movies.Validators.Film;
using test.movies.Constants;
using test.movies.Mocks;

namespace test.movies.Validators
{
    public class FilmCreateValidatorTest
    {
        private FilmCreateValidator FilmCreateValidator { get; set; }

        [SetUp]
        public void Setup()
        {
            var dfm = new DependencyFactoryMock();
            var valdiation = new FilmValidation(dfm);
            FilmCreateValidator = new FilmCreateValidator(valdiation);
        }

        [Test]
        public void Validate([Values(null, "", " ", "undefined_title", TestDataConstants.ExistingFilmTitle)] string? title,
            [Values(null, "", " ", "undefined_rating_type_name", TestDataConstants.ExisitngRatingTypeName)] string? ratingTypeName)
        {
            // Arange
            var filmCreateModel = new FilmCreateModel
            {
                Title = title,
                RatingTypeName = ratingTypeName
            };

            // Act
            var result = FilmCreateValidator.TestValidate(filmCreateModel);

            // Assert
            if (string.IsNullOrWhiteSpace(title))
                result.ShouldHaveValidationErrorFor(model => model.Title)
                    .WithErrorCode(ValidationApiErrorConstants.FILM_TITLE_REQUIRED);
            else
            {
                if (title.Equals(TestDataConstants.ExistingFilmTitle))
                    result.ShouldHaveValidationErrorFor(model => model.Title)
                        .WithErrorCode(ValidationApiErrorConstants.FILM_ALREADY_EXISTS);
                else
                    result.ShouldNotHaveValidationErrorFor(model => model.Title);
            }

            if (string.IsNullOrWhiteSpace(ratingTypeName))
                result.ShouldHaveValidationErrorFor(model => model.RatingTypeName)
                    .WithErrorCode(ValidationApiErrorConstants.RATING_TYPE_NAME_REQUIRED);
            else
            {
                if (!ratingTypeName.Equals(TestDataConstants.ExisitngRatingTypeName))
                    result.ShouldHaveValidationErrorFor(model => model.RatingTypeName)
                        .WithErrorCode(ValidationApiErrorConstants.RATING_TYPE_NAME_INVALID);
                else
                    result.ShouldNotHaveValidationErrorFor(model => model.RatingTypeName);
            }
        }
    }
}
