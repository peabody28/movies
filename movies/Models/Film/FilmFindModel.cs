using Newtonsoft.Json;

namespace movies.Models.Film
{
    public class FilmFindModel
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
