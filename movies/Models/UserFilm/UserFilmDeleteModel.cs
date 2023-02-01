using Newtonsoft.Json;

namespace movies.Models.UserFilm
{
    public class UserFilmDeleteModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
