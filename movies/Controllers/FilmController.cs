using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Interfaces.Operations;
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

        public FilmController(IUserOperation userOperation, IFilmRepository filmRepository, IDirectorRepository directorRepository,
            IRatingTypeRepository ratingTypeRepository, ICountryRepository countryRepository, IUserFilmRepository userFilmRepository,
            ISectionRepository sectionRepository, FilmModelBuilder filmModelBuilder) : base(userOperation)
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
        public UserFilmsModel UserFilms([FromQuery] UserFilmsRequestModel model)
        {
            var section = SectionRepository.Object(model.SectionName);
            var userFilms = UserFilmRepository.Collection(CurrentUser, section);

            return new UserFilmsModel
            {
                SectionName = section?.Name,
                Films = userFilms.Select(userFilm => FilmModelBuilder.Build(userFilm.Film)) 
            };
        }

        [Authorize]
        [HttpPost]
        [Route("/User/Film")]
        public HttpResponseMessage UserFilmCreate(UserFilmAddModel model)
        {
            var film = FilmRepository.Object(model.FilmId);
            var section = SectionRepository.Object(model.SectionName);

            UserFilmRepository.Create(CurrentUser, film, section);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}