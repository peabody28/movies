using movies.Interfaces.Entities;
using movies.Models.UserFilm;

namespace movies.ModelBuilders
{
    public class UserFilmModelBuilder
    {
        public UserFilmModel Build(IUserFilm userFilm)
        {
            return new UserFilmModel
            {
                Id = userFilm.Id,
                FilmId = userFilm.Film.Id,
                SectionName = userFilm.Section?.Name
            };
        }
    }
}
