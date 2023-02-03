using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Interfaces.Operations;
using movies.Interfaces.Repositories;
using movies.ModelBuilders;
using movies.Models.Common;
using movies.Models.Film;

namespace movies.Controllers
{
    public class FilmController : BaseController
    {
        #region [ Dependnecy -> Repositories ]

        public IFilmRepository FilmRepository { get; set; }

        public IDirectorRepository DirectorRepository { get; set; }

        public IRatingTypeRepository RatingTypeRepository { get; set; }

        public ICountryRepository CountryRepository { get; set; }

        #endregion

        #region [ Dependency -> Model Builders ]

        public FilmModelBuilder FilmModelBuilder { get; set; }

        #endregion

        public FilmController(IUserOperation userOperation, IFilmRepository filmRepository, IDirectorRepository directorRepository,
            IRatingTypeRepository ratingTypeRepository, ICountryRepository countryRepository, FilmModelBuilder filmModelBuilder) : base(userOperation)
        {
            FilmRepository = filmRepository;
            DirectorRepository = directorRepository;
            RatingTypeRepository = ratingTypeRepository;
            CountryRepository = countryRepository;
            FilmModelBuilder = filmModelBuilder;
        }

        [Authorize]
        [HttpPost]
        public FilmModel Create(FilmCreateModel model)
        {
            var director = DirectorRepository.Object(model.DirectorName);
            if(director == null) 
                director = DirectorRepository.Create(model.DirectorName);

            var ratingType = RatingTypeRepository.Object(model.RatingTypeName);

            var country = CountryRepository.Object(model.CountryCode);
            if(country == null)
                country = CountryRepository.Create(model.CountryCode);

            var film = FilmRepository.Create(director, ratingType!, model.RatingValue, country, model.Title, model.Description, model.Year);

            return FilmModelBuilder.Build(film);
        }

        [Authorize]
        [HttpGet]
        public PaginationResponseModel<FilmModel> Get([FromQuery] FilmGetModel model)
        {
            var films = FilmRepository.Collection(model.PageSize, model.PageNumber);

            return new PaginationResponseModel<FilmModel>
            {
                TotalCount = FilmRepository.Count(),
                Collection = films.Select(film => FilmModelBuilder.Build(film))
            };
        }
    }
}