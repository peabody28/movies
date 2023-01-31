using Newtonsoft.Json;

namespace movies.Models.Film
{
    public class FilmUpdateModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
