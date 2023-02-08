using movies.Attributes;
using movies.Constants;
using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;
using movies.Validators;

namespace movies.Validations.Film
{
    public class FilmValidation
    {
        #region [ Dependency -> Repositories ]

        [Dependency]
        public IRatingTypeRepository RatingTypeRepository { get; set; }

        [Dependency]
        public IFilmRepository FilmRepository { get; set; }

        [Dependency]
        public ISectionRepository SectionRepository { get; set; }

        [Dependency]
        public IUserFilmRepository UserFilmRepository { get; set; }

        #endregion

        public FilmValidation(DependencyFactory dependencyFactory)
        {
            dependencyFactory.ResolveDependency(this);
        }

        public ValidationResult ValidateRatingTypeName(string? name)
        {
            if(string.IsNullOrWhiteSpace(name))
                return new ValidationResult(ValidationApiErrorConstants.RATING_TYPE_NAME_REQUIRED, "Rating type name is required");

            var ratingType = RatingTypeRepository.Object(name);
            if (ratingType == null)
                return new ValidationResult(ValidationApiErrorConstants.RATING_TYPE_INVALID, "Cannot find a rating type by specified name");
            
            return ValidationResult.Empty();
        }

        public ValidationResult ValidateFilmId(Guid id)
        {
            var film = FilmRepository.Object(id);
            if (film == null)
                return new ValidationResult(ValidationApiErrorConstants.FILM_ID_INVALID, "Cannot find a film with specified ID");

            return ValidationResult.Empty();
        }

        public ValidationResult ValidateUserFilmId(Guid id)
        {
            var userFilm = UserFilmRepository.Object(id);
            if(userFilm == null)
                return new ValidationResult(ValidationApiErrorConstants.USER_FILM_ID_INVALID, "Cannot find a user film by specified ID");

            return ValidationResult.Empty();
        }

        public ValidationResult CheckDuplicates(string? filmTitle)
        {
            if(string.IsNullOrWhiteSpace(filmTitle))
                return new ValidationResult(ValidationApiErrorConstants.FILM_TITLE_REQUIRED, "Film title is required");

            var film = FilmRepository.Object(filmTitle);
            if (film != null)
                return new ValidationResult(ValidationApiErrorConstants.FILM_ALREADY_EXISTS, "Film is already exists");

            return ValidationResult.Empty();
        }

        public ValidationResult CheckDuplicates(IUser user, Guid filmId, string? sectionName)
        {
            var film = FilmRepository.Object(filmId);
            var section = !string.IsNullOrWhiteSpace(sectionName) ? SectionRepository.Object(sectionName) : null;

            var userFilm = UserFilmRepository.Object(user, film!, section);
            if(userFilm != null)
                return new ValidationResult(ValidationApiErrorConstants.FILM_ALREADY_EXISTS, "User film is already exists");

            return ValidationResult.Empty();
        }
    }
}
