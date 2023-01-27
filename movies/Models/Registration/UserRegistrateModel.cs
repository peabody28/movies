using Newtonsoft.Json;

namespace movies.Models.Registration
{
    public class UserRegistrateModel
    {
        [JsonProperty("nickName")]
        public string NickName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstName")]
        public string? FirstName { get; set; }

        [JsonProperty("lastName")]
        public string? LastName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
