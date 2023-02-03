using movies.Controllers;
using movies.Models.Registration;
using test.movies.Mocks;

namespace test.movies.Controllers
{
    internal class RegistrationControllerTest
    {
        [SetUp]
        public void Setup()
        {

        }

        // test validator instead
        [Test]
        public void Registrate([Values(null, "", " ", "Test")]string nickname)
        {
            // Arrange
            DependencyFactoryMock dfm = new DependencyFactoryMock();
            var registrationController = new RegistrationController(dfm);

            var userRegistrateModel = new UserRegistrateModel
            {
                NickName = nickname,
                Email = "asd@asd",
                Password = "1234",
            };

            // Act

            var resp = registrationController.Registrate(userRegistrateModel);

            // Assert
        }
    }
}
