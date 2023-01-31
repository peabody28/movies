using Newtonsoft.Json;

namespace movies.Models.Section
{
    public class SectionModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
