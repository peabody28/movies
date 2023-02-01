namespace movies.Validators
{
    public class ValidationError
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public object CustomState { get; set; }
    }
}
