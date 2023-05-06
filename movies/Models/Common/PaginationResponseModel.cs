using Newtonsoft.Json;

namespace movies.Models.Common
{
    public class PaginationResponseModel<T>
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("collection")]
        public IEnumerable<T> Collection { get; set; }
    }
}
