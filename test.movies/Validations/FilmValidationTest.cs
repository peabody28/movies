using movies.Constants;
using movies.Entities;
using movies.Interfaces.Entities;
using movies.Validations.Film;
using movies.Validators;
using test.movies.Constants;
using test.movies.Mocks;

namespace test.movies.Validations
{
    public class FilmValidationTest
    {
        private FilmValidation FilmValidation { get; set; }

        private DependencyFactoryMock DependencyFactoryMock { get; set; }

        [SetUp]
        public void Setup()
        {
            DependencyFactoryMock = new DependencyFactoryMock();
            FilmValidation = new FilmValidation(DependencyFactoryMock);
        }

        [Test]
        public void ValidateFilmValidateRatingTypeName([Values(null, "", " ", "undefined_rating_type", TestDataConstants.ExisitngRatingTypeName)] string? ratingTypeName)
        {
            // Arrange

            // Act
            var resp = FilmValidation.ValidateRatingTypeName(ratingTypeName);

            // Assert
            Assert.IsNotNull(resp);
            Assert.IsInstanceOf<ValidationResult>(resp);

            if (string.IsNullOrWhiteSpace(ratingTypeName))
            {
                Assert.IsTrue(resp.HasErrors);
                Assert.IsTrue(resp.Errors.Select(item => item.Code).Contains(ValidationApiErrorConstants.RATING_TYPE_NAME_REQUIRED));
            }
            else
            {
                if (ratingTypeName.Equals(TestDataConstants.ExisitngRatingTypeName))
                    Assert.IsTrue(resp.IsValid);
                else
                {
                    Assert.IsTrue(resp.HasErrors);
                    Assert.IsTrue(resp.Errors.Select(item => item.Code).Contains(ValidationApiErrorConstants.RATING_TYPE_INVALID));
                }
            }
        }

        [Test]
        public void ValidateFilmId([Values("{00000000-0000-0000-0000-000000000000}", TestDataConstants.ExistingFilmId)] string id)
        {
            // Arrange
            var filmId = Guid.Parse(id);

            // Act
            var resp = FilmValidation.ValidateFilmId(filmId);

            // Assert
            Assert.IsNotNull(resp);
            Assert.IsInstanceOf<ValidationResult>(resp);

            if (filmId.Equals(Guid.Parse(TestDataConstants.ExistingFilmId)))
                Assert.IsTrue(resp.IsValid);
            else
            {
                Assert.IsTrue(resp.HasErrors);
                Assert.IsTrue(resp.Errors.Select(item => item.Code).Contains(ValidationApiErrorConstants.FILM_ID_INVALID));
            }
        }

        [Test]
        public void ValidateUserFilmId([Values("{00000000-0000-0000-0000-000000000000}", TestDataConstants.ExistingUserFilmId)] string id)
        {
            // Arrange
            var userFilmId = Guid.Parse(id);

            // Act
            var resp = FilmValidation.ValidateUserFilmId(userFilmId);

            // Assert
            Assert.IsNotNull(resp);
            Assert.IsInstanceOf<ValidationResult>(resp);

            if (userFilmId.Equals(Guid.Parse(TestDataConstants.ExistingUserFilmId)))
                Assert.IsTrue(resp.IsValid);
            else
            {
                Assert.IsTrue(resp.HasErrors);
                Assert.IsTrue(resp.Errors.Select(item => item.Code).Contains(ValidationApiErrorConstants.USER_FILM_ID_INVALID));
            }
        }

        [Test]
        public void CheckDuplicates([Values(null, "", " ", "undefined_title", TestDataConstants.ExistingFilmTitle)] string? filmTitle)
        {
            // Act
            var resp = FilmValidation.CheckDuplicates(filmTitle);

            // Assert
            Assert.IsNotNull(resp);
            Assert.IsInstanceOf<ValidationResult>(resp);

            if (string.IsNullOrWhiteSpace(filmTitle))
            {
                Assert.IsTrue(resp.HasErrors);
                Assert.IsTrue(resp.Errors.Select(item => item.Code).Contains(ValidationApiErrorConstants.FILM_TITLE_REQUIRED));
            }
            else
            {
                if(filmTitle.Equals(TestDataConstants.ExistingFilmTitle))
                {
                    Assert.IsTrue(resp.HasErrors);
                    Assert.IsTrue(resp.Errors.Select(item => item.Code).Contains(ValidationApiErrorConstants.FILM_ALREADY_EXISTS));
                }
                else
                    Assert.IsTrue(resp.IsValid);
            }
        }

        
        [Test]
        public void CheckDuplicates([Values("undefined_user_film_user", TestDataConstants.ExistingUserName)] string nickName,
            [Values("{00000000-0000-0000-0000-000000000000}", TestDataConstants.ExistingFilmId)] string id,
            [Values("undefined_user_film_section", TestDataConstants.ExistingSectionName)] string sectionName)
        {
            // Arrange
            var existingUserFilmStub = DependencyFactoryMock.ExistingUserFilmStub;
            var user = nickName.Equals(TestDataConstants.ExistingUserName) ? DependencyFactoryMock.ExistingUserStub : new UserEntity { };
            var filmId = Guid.Parse(id);

            // Act
            var resp = FilmValidation.CheckDuplicates(user, filmId, sectionName);

            // Assert
            Assert.IsNotNull(resp);
            Assert.IsInstanceOf<ValidationResult>(resp);

            if (nickName.Equals(existingUserFilmStub.User.NickName) &&
                filmId.Equals(existingUserFilmStub.Film.Id) &&
                sectionName.Equals(existingUserFilmStub.Section!.Name))
            {
                Assert.IsTrue(resp.HasErrors);
                Assert.IsTrue(resp.Errors.Select(item => item.Code).Contains(ValidationApiErrorConstants.FILM_ALREADY_EXISTS));
            }
            else
                Assert.IsTrue(resp.IsValid);
        }
        
    }
}
