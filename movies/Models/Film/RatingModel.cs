using Newtonsoft.Json;

namespace movies.Models.Film
{
    public class RatingModel
    {
        [JsonProperty("ratingTypeName")]
        public string RatingTypeName { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }
    }
}
