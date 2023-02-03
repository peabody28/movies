using Newtonsoft.Json;

namespace movies.Models.Common
{
    public class PaginationModel
    {
        [JsonProperty("pageNumber")]
        public int PageNumber { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
    }
}
