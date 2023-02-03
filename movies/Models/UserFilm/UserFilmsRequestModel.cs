using movies.Models.Common;
using Newtonsoft.Json;

namespace movies.Models.UserFilm
{
    public class UserFilmsRequestModel : PaginationModel
    {
        [JsonProperty("sectionName")]
        public string? SectionName { get; set; }
    }
}
