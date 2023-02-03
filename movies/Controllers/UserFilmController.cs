using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Interfaces.Operations;
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

        public ISectionRepository SectionRepository { get; set; }

        public IUserFilmRepository UserFilmRepository { get; set; }

        public IFilmRepository FilmRepository { get; set; }

        #endregion

        #region [ Dependency -> Model Builders ]

        public UserFilmModelBuilder UserFilmModelBuilder { get; set; }

        #endregion

        public UserFilmController(IUserOperation userOperation, UserFilmModelBuilder userFilmModelBuilder, ISectionRepository sectionRepository,
            IUserFilmRepository userFilmRepository, IFilmRepository filmRepository) : base(userOperation)
        {
            SectionRepository = sectionRepository;
            UserFilmRepository = userFilmRepository;
            FilmRepository = filmRepository;
            UserFilmModelBuilder = userFilmModelBuilder;
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

            var userFilms = UserFilmRepository.Collection(CurrentUser, model.PageSize, model.PageNumber, section);

            return new PaginationResponseModel<UserFilmModel>
            {
                TotalCount = UserFilmRepository.Count(CurrentUser, section),
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
