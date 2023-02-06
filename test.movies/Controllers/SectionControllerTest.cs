using movies;
using movies.Controllers;
using test.movies.Mocks;

namespace test.movies.Controllers
{
    internal class SectionControllerTest
    {
       
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Get()
        {
            // Arrange
            DependencyFactoryMock dfm = new DependencyFactoryMock();
            var sectionController = new SectionController(dfm);

            // Act
            var resp = sectionController.Get();

            // Assert
            Assert.IsNotNull(resp);

        }
    }
}
