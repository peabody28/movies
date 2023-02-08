using movies.Attributes;
using movies.Constants;
using movies.Interfaces.Repositories;
using movies.Validators;

namespace movies.Validations.Section
{
    public class SectionValidation
    {
        #region [ Dependency -> Repositories ]

        [Dependency]
        public ISectionRepository SectionRepository { get; set; }

        #endregion

        public SectionValidation(DependencyFactory dependencyFactory)
        {
            dependencyFactory.ResolveDependency(this);
        }

        public ValidationResult Validate(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ValidationResult.Empty();

            var section = SectionRepository.Object(name);
            if (section == null)
                return new ValidationResult(ValidationApiErrorConstants.SECTION_INVALID, "Cannot find a section by specified name");

            return ValidationResult.Empty();
        }
    }
}
