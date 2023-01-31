using Newtonsoft.Json;

namespace movies.Models.Film
{
    public class UserFilmsModel
    {
        [JsonProperty("sectionName")]
        public string? SectionName { get; set; }

        [JsonProperty("films")]
        public IEnumerable<FilmModel> Films { get; set; }
    }
}
