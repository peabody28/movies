using Newtonsoft.Json;

namespace movies.Models.Film
{
    public class UserFilmsRequestModel
    {
        [JsonProperty("sectionName")]
        public string? SectionName { get; set; }
    }
}
