using movies.Models.Film;
using Newtonsoft.Json;

namespace movies.Models.UserFilm
{
    public class UserFilmModel
    {
        [JsonProperty("id")]
        public new Guid Id { get; set; }

        [JsonProperty("filmId")]
        public Guid FilmId { get; set;}

        [JsonProperty("sectionName")]
        public string? SectionName { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("directorName")]
        public string DirectorName { get; set; }

        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }

        [JsonProperty("ratings")]
        public IEnumerable<RatingModel> Ratings { get; set; }
    }
}
