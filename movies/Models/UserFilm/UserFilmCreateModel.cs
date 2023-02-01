using Newtonsoft.Json;

namespace movies.Models.UserFilm
{
    public class UserFilmCreateModel
    {
        [JsonProperty("filmId")]
        public Guid FilmId { get; set; }

        [JsonProperty("sectionName")]
        public string? SectionName { get; set; }
    }
}
