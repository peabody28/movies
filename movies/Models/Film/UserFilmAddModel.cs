using Newtonsoft.Json;

namespace movies.Models.Film
{
    public class UserFilmAddModel
    {
        [JsonProperty("filmId")]
        public Guid FilmId { get; set; }
    }
}
