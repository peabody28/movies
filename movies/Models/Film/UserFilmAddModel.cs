using Newtonsoft.Json;

namespace movies.Models.Film
{
    public class UserFilmAddModel
    {
        [JsonProperty("filmId")]
        public Guid FilmId { get; set; }

        [JsonProperty("sectionName")]
        public string? SectionName { get; set; }
    }
}
