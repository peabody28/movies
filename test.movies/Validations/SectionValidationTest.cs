using movies.Constants;
using movies.Validations.Section;
using movies.Validators;
using test.movies.Constants;
using test.movies.Mocks;

namespace test.movies.Validations
{
    public class SectionValidationTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ValidateName([Values(null, "", " ", "undefined_section_name", TestDataConstants.ExistingSectionName)] string? sectionName)
        {
            // Arrange
            DependencyFactoryMock dfm = new DependencyFactoryMock();
            var sectionValidation = new SectionValidation(dfm);

            // Act
            var resp = sectionValidation.ValidateName(sectionName);

            // Assert
            Assert.IsNotNull(resp);
            Assert.IsInstanceOf<ValidationResult>(resp);

            if (string.IsNullOrWhiteSpace(sectionName))
                Assert.IsTrue(resp.IsValid);
            else
            {
                if(sectionName.Equals(TestDataConstants.ExistingSectionName))
                    Assert.IsTrue(resp.IsValid);
                else
                {
                    Assert.IsTrue(resp.HasErrors);
                    Assert.IsTrue(resp.Errors.Select(item => item.Code).Contains(ValidationApiErrorConstants.SECTION_NAME_INVALID));
                }
            }
        }
    }
}
