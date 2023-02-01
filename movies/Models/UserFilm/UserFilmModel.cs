using Newtonsoft.Json;

namespace movies.Models.UserFilm
{
    public class UserFilmModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("filmId")]
        public Guid FilmId { get; set; }

        [JsonProperty("sectionName")]
        public string? SectionName { get; set; }
    }
}
