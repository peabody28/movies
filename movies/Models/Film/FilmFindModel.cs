using movies.Models.Common;
using Newtonsoft.Json;

namespace movies.Models.Film
{
    public class UserFilmFindModel : PaginationModel
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
