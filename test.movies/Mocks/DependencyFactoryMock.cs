using Moq;
using movies;
using movies.Attributes;
using movies.Entities;
using movies.Helpers;
using movies.Interfaces.Entities;
using movies.Interfaces.Operations;
using movies.Interfaces.Repositories;
using movies.ModelBuilders;
using movies.Models.Login;
using System.Security.Claims;
using test.movies.Constants;

namespace test.movies.Mocks
{
    internal class DependencyFactoryMock : DependencyFactory
    {

        public DependencyFactoryMock() : base(null!)
        {

        }

        public override void ResolveDependency(object instance)
        {
            var properties = instance.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (Attribute.IsDefined(property, typeof(DependencyAttribute)))
                {
                    var value = GetMock(property.PropertyType);
                    property.SetValue(instance, value);
                }
            }
        }


        public IUser ExistingUserStub = new UserEntity { Id = Guid.Parse(TestDataConstants.ExistingUserId), Email = "asd@asd", NickName = TestDataConstants.ExistingUserName, PasswordHash = MD5Helper.Hash(TestDataConstants.ExistingUserPassword) };

        private IFilm ExistingFilmStub = new FilmEntity { Id = Guid.Parse(TestDataConstants.ExistingFilmId), Title = TestDataConstants.ExistingFilmTitle };

        private ISection ExistingSectionStub = new SectionEntity { Id = Guid.Parse(TestDataConstants.ExistingSectionId), Name = TestDataConstants.ExistingSectionName };
        
        public IUserFilm ExistingUserFilmStub
        { 
            get 
            { 
                return new UserFilmEntity 
                { 
                    Id = Guid.Parse(TestDataConstants.ExistingUserFilmId),
                    User = ExistingUserStub,
                    Film = ExistingFilmStub,
                    Section = ExistingSectionStub
                }; 
            } 
        }

        private IUserOperation UserOperationMock()
        {
            var userOperationMock = new Mock<IUserOperation>();
            userOperationMock.Setup(a => a.CurrentUser).Returns(ExistingUserStub);
            return userOperationMock.Object;
        }

        private IIdentityOperation IdentityOperationMock()
        {
            var identity = new ClaimsIdentity();
            var mock = new Mock<IIdentityOperation>();
            mock.Setup(m => m.Object(It.IsAny<string>(), It.IsAny<string>())).Returns((ClaimsIdentity?)null);
            mock.Setup(m => m.Object(ExistingUserStub.NickName, TestDataConstants.ExistingUserPassword)).Returns(identity);

            return mock.Object;
        }

        private IAuthorizationOperation AuthorizationOperationMock()
        {
            var mock = new Mock<IAuthorizationOperation>();
            var tokenModel = new TokenDtoModel { AccessToken = "some_token" };

            mock.Setup(m => m.GenerateToken(It.IsAny<ClaimsIdentity>())).Returns(tokenModel);
            return mock.Object;
        }

        private ISectionRepository SectionRepositoryMock()
        {
            var sectionRepositoryMock = new Mock<ISectionRepository>();
            sectionRepositoryMock.Setup(a => a.Collection()).Returns(new List<ISection> { ExistingSectionStub } );
            sectionRepositoryMock.Setup(m => m.Object(It.IsAny<string>())).Returns((ISection)null);
            sectionRepositoryMock.Setup(m => m.Object(ExistingSectionStub.Name)).Returns(ExistingSectionStub);

            return sectionRepositoryMock.Object;
        }

        private IRatingTypeRepository RatingTypeRepositoryMock()
        {
            var entity = new RatingTypeEntity { Name = TestDataConstants.ExisitngRatingTypeName };
            var ratingTypeRepositoryMock = new Mock<IRatingTypeRepository>();
            ratingTypeRepositoryMock.Setup(a => a.Object(It.IsAny<string>())).Returns((IRatingType)null);
            ratingTypeRepositoryMock.Setup(a => a.Object(TestDataConstants.ExisitngRatingTypeName)).Returns(entity);

            return ratingTypeRepositoryMock.Object;
        }

        private IFilmRepository FilmRepositoryMock()
        {
            var filmRepositoryMock = new Mock<IFilmRepository>();
            filmRepositoryMock.Setup(a => a.Object(It.IsAny<Guid>())).Returns((IFilm?)null);
            filmRepositoryMock.Setup(a => a.Object(ExistingFilmStub.Id)).Returns(ExistingFilmStub);
            filmRepositoryMock.Setup(a => a.Object(It.IsAny<string>())).Returns((IFilm?)null);
            filmRepositoryMock.Setup(a => a.Object(ExistingFilmStub.Title)).Returns(ExistingFilmStub);

            return filmRepositoryMock.Object;
        }

        private IUserRepository UserRepositoryMock()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(m => m.Object(It.IsAny<string>())).Returns((IUser)null);
            mock.Setup(m => m.Object(It.IsAny<string>(), It.IsAny<string>())).Returns((IUser)null);
            mock.Setup(m => m.Object(ExistingUserStub.NickName)).Returns(ExistingUserStub);
            mock.Setup(m => m.Object(ExistingUserStub.NickName, ExistingUserStub.PasswordHash)).Returns(ExistingUserStub);
            mock.Setup(m => m.Create(It.IsNotNull<string>(), It.IsNotNull<string>(), It.IsNotNull<string>())).Returns(ExistingUserStub);
            return mock.Object;
        }

        private IUserFilmRepository UserFilmRepositoryMock()
        {
            var mock = new Mock<IUserFilmRepository>();
            int count = 0;
            mock.Setup(m => m.Collection(It.IsAny<IUser>(), It.IsAny<int>(), It.IsAny<int>(), out count, It.IsAny<ISection>(), It.IsAny<bool>())).Returns(new List<IUserFilm>());
            mock.Setup(a => a.Object(It.IsAny<Guid>(), It.IsAny<bool>())).Returns((IUserFilm?)null);
            mock.Setup(a => a.Object(ExistingUserFilmStub.Id, It.IsAny<bool>())).Returns(ExistingUserFilmStub);
            mock.Setup(a => a.Object(It.IsAny<IUser>(), It.IsAny<IFilm>(), It.IsAny<ISection>(), It.IsAny<bool>())).Returns((IUserFilm?)null);
            mock.Setup(a => a.Object(ExistingUserFilmStub.User, ExistingUserFilmStub.Film, ExistingUserFilmStub.Section, It.IsAny<bool>())).Returns(ExistingUserFilmStub);

            return mock.Object;
        }

        private UserFilmModelBuilder UserFilmModelBuilderMock()
        {
            return new UserFilmModelBuilder(this);
        }
        
        private FilmModelBuilder FilmModelBuilderMock()
        {
            return new FilmModelBuilder(this);
        }

        public dynamic GetMock(Type instance)
        {
            if (instance.Equals(typeof(IUserOperation))) return UserOperationMock();
            else if (instance.Equals(typeof(IIdentityOperation))) return IdentityOperationMock();
            else if (instance.Equals(typeof(IAuthorizationOperation))) return AuthorizationOperationMock();
            else if (instance.Equals(typeof(ISectionRepository))) return SectionRepositoryMock();
            else if (instance.Equals(typeof(IUserRepository)))return UserRepositoryMock();
            else if (instance.Equals(typeof(IUserFilmRepository))) return UserFilmRepositoryMock();
            else if (instance.Equals(typeof(IRatingTypeRepository))) return RatingTypeRepositoryMock();
            else if (instance.Equals(typeof(IFilmRepository))) return FilmRepositoryMock();
            else if (instance.Equals(typeof(UserFilmModelBuilder))) return UserFilmModelBuilderMock();
            else if (instance.Equals(typeof(FilmModelBuilder))) return FilmModelBuilderMock();

            return null!;
        }
    }
}
