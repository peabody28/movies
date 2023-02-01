using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;
using movies.Validators;

namespace movies.Validations.Film
{
    public class FilmValidation
    {
        #region [ Dependency -> Repositories ]

        public IRatingTypeRepository RatingTypeRepository { get; set; }

        public IFilmRepository FilmRepository { get; set; }

        public ISectionRepository SectionRepository { get; set; }

        public IUserFilmRepository UserFilmRepository { get; set; }

        #endregion

        public FilmValidation(IRatingTypeRepository ratingTypeRepository, IFilmRepository filmRepository,
            ISectionRepository sectionRepository, IUserFilmRepository userFilmRepository) 
        {
            RatingTypeRepository = ratingTypeRepository;
            FilmRepository = filmRepository;
            SectionRepository = sectionRepository;
            UserFilmRepository = userFilmRepository;
        }

        public ValidationResult ValidateRatingTypeName(string name)
        {
            var ratingType = RatingTypeRepository.Object(name);
            if (ratingType == null)
                return new ValidationResult("RATING_TYPE_INVALID", "Cannot find a rating type by specified name");
            
            return ValidationResult.Empty();
        }

        public ValidationResult ValidateFilmId(Guid id)
        {
            var film = FilmRepository.Object(id);
            if (film == null)
                return new ValidationResult("FILM_ID_INVALID", "Cannot find a film with specified ID");

            return ValidationResult.Empty();
        }

        public ValidationResult CheckDuplicates(string filmTitle)
        {
            var film = FilmRepository.Object(filmTitle);
            if (film != null)
                return new ValidationResult("FILM_ALREADY_EXISTS", "Film is already exists");

            return ValidationResult.Empty();
        }

        public ValidationResult CheckDuplicates(IUser user, Guid filmId, string? sectionName)
        {
            var film = FilmRepository.Object(filmId);
            var section = !string.IsNullOrWhiteSpace(sectionName) ? SectionRepository.Object(sectionName) : null;

            var userFilm = UserFilmRepository.Object(user, film!, section);
            if(userFilm != null)
                return new ValidationResult("FILM_ALREADY_EXISTS", "Film is already exists");

            return ValidationResult.Empty();
        }
    }
}
