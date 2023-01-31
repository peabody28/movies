using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Interfaces.Repositories;
using movies.ModelBuilders;
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

        public ISectionRepository SectionRepository { get; set; }

        #endregion


        #region [ Dependency -> Model Builders ]

        public FilmModelBuilder FilmModelBuilder { get; set; }

        #endregion

        public FilmController(IUserRepository userRepository, IFilmRepository filmRepository, IDirectorRepository directorRepository,
            IRatingTypeRepository ratingTypeRepository, ICountryRepository countryRepository, IUserFilmRepository userFilmRepository,
            ISectionRepository sectionRepository, FilmModelBuilder filmModelBuilder) : base(userRepository)
        {
            FilmRepository = filmRepository;
            DirectorRepository = directorRepository;
            RatingTypeRepository = ratingTypeRepository;
            CountryRepository = countryRepository;
            UserFilmRepository = userFilmRepository;
            SectionRepository = sectionRepository;
            FilmModelBuilder = filmModelBuilder;
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

            return FilmModelBuilder.Build(film);
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<FilmModel> Get()
        {
            var films = FilmRepository.Collection();

            return films.Select(film => FilmModelBuilder.Build(film));
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
            var section = SectionRepository.Object(model.SectionName);

            UserFilmRepository.Create(CurrentUser, film, section);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}