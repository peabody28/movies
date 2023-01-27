using Newtonsoft.Json;

namespace movies.Models.Login
{
    public class UserAuthorizeModel
    {
        [JsonProperty("nickName")]
        public string NickName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
