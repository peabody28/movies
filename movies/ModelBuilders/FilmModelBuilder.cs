using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;
using movies.Models.Film;

namespace movies.ModelBuilders
{
    public class FilmModelBuilder
    {
        public IRatingRepository RatingRepository { get; set; }

        public FilmModelBuilder(IRatingRepository ratingRepository) 
        {
            RatingRepository = ratingRepository;
        }

        public FilmModel Build(IFilm film)
        {
            var ratings = RatingRepository.Collection(film);

            return new FilmModel
            {
                Id = film.Id,
                Title = film.Title,
                Description = film.Description,
                DirectorName = string.Join(" ", film.Director.FirstName, film.Director.LastName),
                CountryName = film.Country.Name,
                Year = film.Year,
                Ratings = ratings.Select(r => new RatingModel
                {
                    RatingTypeName = r.RatingType.Name,
                    Value = r.Value
                })
            };
        }
    }
}
