using movies.Interfaces.Entities;
using movies.Models.UserFilm;

namespace movies.ModelBuilders
{
    public class UserFilmModelBuilder
    {
        public FilmModelBuilder FilmModelBuilder { get; set; }

        public UserFilmModelBuilder(FilmModelBuilder filmModelBuilder) 
        {
            FilmModelBuilder = filmModelBuilder;
        }

        public UserFilmModel Build(IUserFilm userFilm)
        {
            var filmModel = FilmModelBuilder.Build(userFilm.Film);

            return new UserFilmModel
            {
                Id = userFilm.Id,
                FilmId = filmModel.Id,
                SectionName = userFilm.Section?.Name,
                Title = filmModel.Title,
                Description = filmModel.Description,
                DirectorName = filmModel.DirectorName,
                CountryName = filmModel.CountryName,
                Year = filmModel.Year,
                Ratings = filmModel.Ratings
            };
        }
    }
}
