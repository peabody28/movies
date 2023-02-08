using FluentValidation.TestHelper;
using movies.Constants;
using movies.Interfaces.Operations;
using movies.Models.Film;
using movies.Models.UserFilm;
using movies.Validations.Film;
using movies.Validations.Section;
using movies.Validators.Film;
using movies.Validators.UserFilm;
using System;
using test.movies.Constants;
using test.movies.Mocks;

namespace test.movies.Validators
{
    public class UserFilmCreateValidatorTest
    {
        private UserFilmCreateValidator UserFilmCreateValidator { get; set; }

        [SetUp]
        public void Setup()
        {
            var dfm = new DependencyFactoryMock();
            var filmValdiation = new FilmValidation(dfm);
            var sectionValidation = new SectionValidation(dfm);
            var userOperation = dfm.GetMock(typeof(IUserOperation));
            UserFilmCreateValidator = new UserFilmCreateValidator(filmValdiation, sectionValidation, userOperation);
        }

        [Test]
        public void Validate([Values("{00000000-0000-0000-0000-000000000000}", TestDataConstants.ExistingFilmId, TestDataConstants.ExistingUserFilmFilmId)] string? filmId,
            [Values(null, "", " ", "undefined_section_name", TestDataConstants.ExistingSectionName, TestDataConstants.ExistingUserFilmSectionName)] string? sectionName)
        {
            // Arange
            var userFilmCreateModel = new UserFilmCreateModel
            {
                FilmId = Guid.Parse(filmId),
                SectionName = sectionName
            };

            // Act
            var result = UserFilmCreateValidator.TestValidate(userFilmCreateModel);

            // Assert
            if (string.IsNullOrWhiteSpace(sectionName))
                result.ShouldNotHaveValidationErrorFor(model => model.SectionName);
            else
            {
                if (!new[] { TestDataConstants.ExistingUserFilmSectionName, TestDataConstants.ExistingSectionName }.Contains(sectionName))
                    result.ShouldHaveValidationErrorFor(model => model.SectionName)
                        .WithErrorCode(ValidationApiErrorConstants.SECTION_NAME_INVALID);
                else
                    result.ShouldNotHaveValidationErrorFor(model => model.SectionName);
            }

            if (! new[] { TestDataConstants.ExistingUserFilmFilmId, TestDataConstants.ExistingFilmId }.Contains(filmId))
                result.ShouldHaveValidationErrorFor(model => model.FilmId)
                    .WithErrorCode(ValidationApiErrorConstants.FILM_ID_INVALID);
            else
            {
                if (filmId.Equals(TestDataConstants.ExistingUserFilmFilmId) && !string.IsNullOrWhiteSpace(sectionName) && sectionName.Equals(TestDataConstants.ExistingUserFilmSectionName))
                    result.ShouldHaveValidationErrorFor(model => model.FilmId)
                        .WithErrorCode(ValidationApiErrorConstants.FILM_ALREADY_EXISTS);
                else
                    result.ShouldNotHaveValidationErrorFor(model => model.FilmId);
            }
        }
    }
}
