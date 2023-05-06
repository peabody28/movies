using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Attributes;
using movies.Interfaces.Repositories;
using movies.ModelBuilders;
using movies.Models.Common;
using movies.Models.UserFilm;
using System.Net;

namespace movies.Controllers
{
    public class UserFilmController : BaseController
    {
        #region [ Dependency -> Repositories ]

        [Dependency]
        public ISectionRepository SectionRepository { get; set; }

        [Dependency]
        public IUserFilmRepository UserFilmRepository { get; set; }

        [Dependency]
        public IFilmRepository FilmRepository { get; set; }

        #endregion

        #region [ Dependency -> Model Builders ]

        [Dependency]
        public UserFilmModelBuilder UserFilmModelBuilder { get; set; }

        #endregion

        public UserFilmController(DependencyFactory dependencyFactory)
        {
            dependencyFactory.ResolveDependency(this);
        }

        [Authorize]
        [HttpPost]
        [Route("/User/Film")]
        public UserFilmModel Create(UserFilmCreateModel model)
        {
            var film = FilmRepository.Object(model.FilmId);
            var section = !string.IsNullOrWhiteSpace(model.SectionName) ? SectionRepository.Object(model.SectionName) : null;

            var userFilm = UserFilmRepository.Create(CurrentUser, film!, section);

            return UserFilmModelBuilder.Build(userFilm);
        }

        [Authorize]
        [HttpGet]
        [Route("/User/Film")]
        public PaginationResponseModel<UserFilmModel> Get([FromQuery] UserFilmsRequestModel model)
        {
            var section = !string.IsNullOrWhiteSpace(model.SectionName) ? SectionRepository.Object(model.SectionName) : null;

            var userFilms = UserFilmRepository.Collection(CurrentUser, model.PageSize, model.PageNumber, out var totalCount, section);

            return new PaginationResponseModel<UserFilmModel>
            {
                TotalCount = totalCount,
                Collection = userFilms.Select(UserFilmModelBuilder.Build)
            };
        }

        [Authorize]
        [HttpGet]
        [Route("/User/Film/Find")]
        public PaginationResponseModel<UserFilmModel> Find([FromQuery] UserFilmFindModel model)
        {
            var section = !string.IsNullOrWhiteSpace(model.SectionName) ? SectionRepository.Object(model.SectionName) : null;

            var userFilms = UserFilmRepository.Collection(CurrentUser, model.Text, model.PageSize, model.PageNumber, out var totalCount, section);

            return new PaginationResponseModel<UserFilmModel>
            {
                TotalCount = totalCount,
                Collection = userFilms.Select(UserFilmModelBuilder.Build)
            };
        }

        [Authorize]
        [HttpDelete]
        [Route("/User/Film")]
        public HttpResponseMessage Delete(UserFilmDeleteModel model)
        {
            var userFilm = UserFilmRepository.Object(model.Id);

            UserFilmRepository.Delete(userFilm);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
