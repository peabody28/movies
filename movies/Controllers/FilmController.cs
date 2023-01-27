using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies.Interfaces.Repositories;
using movies.Models.Film;

namespace movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : ControllerBase
    {
        public IFilmRepository FilmRepository { get; set; }

        public IDirectorRepository DirectorRepository { get; set; }

        public IRatingTypeRepository RatingTypeRepository { get; set; }

        public ICountryRepository CountryRepository { get; set; }

        public FilmController(IFilmRepository filmRepository, IDirectorRepository directorRepository,
            IRatingTypeRepository ratingTypeRepository, ICountryRepository countryRepository)
        {
            FilmRepository = filmRepository;
            DirectorRepository = directorRepository;
            RatingTypeRepository = ratingTypeRepository;
            CountryRepository = countryRepository;
        }

        [Authorize]
        [HttpPost]
        public FilmModel Create(FilmCreateModel model)
        {
            var director = DirectorRepository.Object(model.DirectorFirstName, model.DirectorLastName);
            var ratingType = RatingTypeRepository.Object(model.RatingTypeName);
            var country = CountryRepository.Object(model.CountryCode);

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
        public IEnumerable<FilmModel> Get([FromQuery] FilmRequestModel model)
        {
            var films = FilmRepository.Collection();

            return films.Select(film => new FilmModel
            {
                Title = film.Title,
                Description = film.Description,
                DirectorName = string.Join(" ", film.Director.FirstName, film.Director.LastName),
                CountryName = film.Country.Name,
                Year = film.Year,
            });
        }
    }
}