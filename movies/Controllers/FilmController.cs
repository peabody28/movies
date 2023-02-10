using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Attributes;
using movies.Interfaces.Repositories;
using movies.ModelBuilders;
using movies.Models.Common;
using movies.Models.Film;

namespace movies.Controllers
{
    public class FilmController : BaseController
    {
        #region [ Dependnecy -> Repositories ]

        [Dependency]
        public IFilmRepository FilmRepository { get; set; }

        [Dependency]
        public IDirectorRepository DirectorRepository { get; set; }

        [Dependency]
        public IRatingTypeRepository RatingTypeRepository { get; set; }

        [Dependency]
        public ICountryRepository CountryRepository { get; set; }

        #endregion

        #region [ Dependency -> Model Builders ]

        [Dependency]
        public FilmModelBuilder FilmModelBuilder { get; set; }

        #endregion

        public FilmController(DependencyFactory dependencyFactory)
        {
            dependencyFactory.ResolveDependency(this);
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
            return null;
            var films = FilmRepository.Collection(model.PageSize, model.PageNumber, out int totalCount);

            return new PaginationResponseModel<FilmModel>
            {
                TotalCount = totalCount,
                Collection = films.Select(film => FilmModelBuilder.Build(film))
            };
        }

        [Authorize]
        [HttpGet]
        [Route("Find")]
        public PaginationResponseModel<FilmModel> Find([FromQuery] UserFilmFindModel model)
        {
            var films = FilmRepository.Collection(model.Text, model.PageSize, model.PageNumber, out var totalCount);

            return new PaginationResponseModel<FilmModel>
            {
                TotalCount = totalCount,
                Collection = films.Select(film => FilmModelBuilder.Build(film))
            };
        }
    }
}