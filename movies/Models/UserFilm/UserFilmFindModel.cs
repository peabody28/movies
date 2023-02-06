using movies.Models.Common;
using Newtonsoft.Json;

namespace movies.Models.UserFilm
{
    public class UserFilmFindModel : PaginationModel
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("sectionName")]
        public string? SectionName { get; set; }
    }
}
