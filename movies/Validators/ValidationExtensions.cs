using FluentValidation;
using FluentValidation.Results;

namespace movies.Validators
{
    public static class ValidationExtensions
    {
        public static void AddFailures<T>(this ValidationContext<T> context, string propertyName, ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                context.AddFailure(new ValidationFailure(propertyName, error.Message) { ErrorCode = error.Code, CustomState = error.CustomState });
            }
        }
    }
}
