using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Interfaces.Repositories;
using movies.Models.Film;
using System.Net;

namespace movies.Controllers
{
    public class FilmController : BaseController
    {
        #region [ Dependnecy -> Repositories ]

        public IFilmRepository FilmRepository { get; set; }

        public IDirectorRepository DirectorRepository { get; set; }

        public IRatingTypeRepository RatingTypeRepository { get; set; }

        public ICountryRepository CountryRepository { get; set; }

        public IUserFilmRepository UserFilmRepository { get; set; }

        #endregion

        public FilmController(IUserRepository userRepository, IFilmRepository filmRepository, IDirectorRepository directorRepository,
            IRatingTypeRepository ratingTypeRepository, ICountryRepository countryRepository, IUserFilmRepository userFilmRepository) : base(userRepository)
        {
            FilmRepository = filmRepository;
            DirectorRepository = directorRepository;
            RatingTypeRepository = ratingTypeRepository;
            CountryRepository = countryRepository;
            UserFilmRepository = userFilmRepository;
        }

        [Authorize]
        [HttpPost]
        public FilmModel Create(FilmCreateModel model)
        {
            var director = DirectorRepository.Object(model.DirectorFirstName, model.DirectorLastName);
            if(director == null) 
                director = DirectorRepository.Create(model.DirectorFirstName, model.DirectorLastName);

            var ratingType = RatingTypeRepository.Object(model.RatingTypeName);

            var country = CountryRepository.Object(model.CountryCode);
            if(country == null)
                country = CountryRepository.Create(model.CountryCode);

            var film = FilmRepository.Create(director, ratingType, model.RatingValue, country, model.Title, model.Description, model.Year);

            var directorName = string.Join(" ", film.Director.FirstName, film.Director.LastName);

            return new FilmModel
            {
                Title = film.Title,
                Description = film.Description,
                DirectorName = directorName,
                CountryName = film.Country.Name,
                Year = film.Year,
            };
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<FilmModel> Get()
        {
            var films = FilmRepository.Collection();

            return films.Select(film => new FilmModel
            {
                Id = film.Id,
                Title = film.Title,
                Description = film.Description,
                DirectorName = string.Join(" ", film.Director.FirstName, film.Director.LastName),
                CountryName = film.Country.Name,
                Year = film.Year,
            });
        }

        [Authorize]
        [HttpGet]
        [Route("/User/Film")]
        public IEnumerable<FilmModel> GetUserFilms()
        {
            var userFilms = UserFilmRepository.Collection(CurrentUser);

            return userFilms.Select(userFilm => new FilmModel
            {
                Id = userFilm.Film.Id,
                Title = userFilm.Film.Title,
                Description = userFilm.Film.Description,
                DirectorName = string.Join(" ", userFilm.Film.Director.FirstName, userFilm.Film.Director.LastName),
                CountryName = userFilm.Film.Country.Name,
                Year = userFilm.Film.Year,
            });
        }

        [Authorize]
        [HttpPost]
        [Route("/User/Film")]
        public HttpResponseMessage Add(UserFilmAddModel model)
        {
            var film = FilmRepository.Object(model.FilmId);

            UserFilmRepository.Create(CurrentUser, film);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}