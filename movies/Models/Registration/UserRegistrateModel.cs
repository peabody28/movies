using Newtonsoft.Json;

namespace movies.Models.Registration
{
    public class UserRegistrateModel
    {
        [JsonProperty("nickName")]
        public string NickName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
