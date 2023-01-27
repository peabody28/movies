namespace movies.Models.Login
{
    public class TokenDtoModel
    {
        public string AccessToken { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
