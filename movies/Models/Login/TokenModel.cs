using Newtonsoft.Json;

namespace movies.Models.Login
{
    public class TokenModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expirationDate")]
        public DateTime ExpirationDate { get; set; }
    }
}
