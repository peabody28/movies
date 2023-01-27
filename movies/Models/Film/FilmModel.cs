using Newtonsoft.Json;

namespace movies.Models.Film
{
    public class FilmModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("directorName")]
        public string DirectorName { get; set; }

        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }
    }
}
