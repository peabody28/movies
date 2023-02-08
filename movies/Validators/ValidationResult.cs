namespace movies.Validators
{
    public class ValidationResult
    {
        public static ValidationResult Empty()
        {
            return new ValidationResult();
        }

        private List<ValidationError> _errors;

        public ValidationResult()
        {
            _errors = new List<ValidationError>();
        }

        public ValidationResult(string code, string message, object? customState = null)
        {
            _errors = new List<ValidationError>
            {
                new ValidationError
                {
                    Code = code,
                    Message = message,
                    CustomState = customState
                }
            };
        }

        public IEnumerable<ValidationError> Errors
        {
            get
            {
                return _errors;
            }
        }

        public bool HasErrors
        {
            get
            {
                return _errors.Count != 0;
            }
        }

        public bool IsValid
        {
            get
            {
                return _errors.Count == 0;
            }
        }
    }
}
