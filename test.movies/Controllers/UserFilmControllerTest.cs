using movies;
using movies.Controllers;
using movies.Models.UserFilm;
using test.movies.Mocks;

namespace test.movies.Controllers
{
    internal class UserFilmControllerTest
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
            var userFilmController = new UserFilmController(dfm);

            // Act
            var resp = userFilmController.Get(new UserFilmsRequestModel());

            // Assert
            Assert.IsNotNull(resp);

        }
    }
}
