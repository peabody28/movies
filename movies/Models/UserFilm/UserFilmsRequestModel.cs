using Newtonsoft.Json;

namespace movies.Models.UserFilm
{
    public class UserFilmsRequestModel
    {
        [JsonProperty("sectionName")]
        public string? SectionName { get; set; }
    }
}
