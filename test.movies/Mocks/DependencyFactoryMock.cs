using Microsoft.AspNetCore.Http;
using Moq;
using movies;
using movies.Attributes;
using movies.Entities;
using movies.Interfaces.Entities;
using movies.Interfaces.Operations;
using movies.Interfaces.Repositories;
using movies.ModelBuilders;
using movies.Models.Login;
using movies.Models.UserFilm;
using System.Security.Claims;

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

        private IUserOperation UserOperationMock()
        {
            var userOperationMock = new Mock<IUserOperation>();
            userOperationMock.Setup(a => a.CurrentUser).Returns(new UserEntity());
            return userOperationMock.Object;
        }

        private IIdentityOperation IdentityOperationMock()
        {
            var identity = new ClaimsIdentity();
            var mock = new Mock<IIdentityOperation>();
            mock.Setup(m => m.Object(It.IsNotNull<string>(), It.IsNotNull<string>())).Returns(identity);
            mock.Setup(m => m.Object(null, It.IsNotNull<string>())).Returns((ClaimsIdentity?)null);

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
            sectionRepositoryMock.Setup(a => a.Collection()).Returns(new List<ISection> { new SectionEntity() { Id = Guid.NewGuid(), Name = "SomeName"} });
            return sectionRepositoryMock.Object;
        }

        private IUserRepository UserRepositoryMock()
        {
            var user = new UserEntity() { Id = Guid.NewGuid(), Email = "asd@asd", NickName = "nickname", PasswordHash = "789456123" };
            var mock = new Mock<IUserRepository>();
            mock.Setup(m=>m.Object(It.IsNotNull<string>())).Returns(user);
            mock.Setup(m => m.Object(It.IsNotNull<string>(), It.IsNotNull<string>())).Returns(user);
            mock.Setup(m => m.Object(null, It.IsNotNull<string>())).Returns((IUser)null);
            mock.Setup(m => m.Create(It.IsNotNull<string>(), It.IsNotNull<string>(), It.IsNotNull<string>())).Returns(user);
            return mock.Object;
        }

        private IUserFilmRepository UserFilmRepositoryMock()
        {
            var user = new UserEntity() { Id = Guid.NewGuid(), Email = "asd@asd", NickName = "nickname", PasswordHash = "789456123" };
            var mock = new Mock<IUserFilmRepository>();
            int count = 0;
            mock.Setup(m => m.Collection(It.IsAny<IUser>(), It.IsAny<int>(), It.IsAny<int>(), out count, It.IsAny<ISection>(), It.IsAny<bool>())).Returns(new List<IUserFilm>());

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
            else if (instance.Equals(typeof(UserFilmModelBuilder))) return UserFilmModelBuilderMock();
            else if (instance.Equals(typeof(FilmModelBuilder))) return FilmModelBuilderMock();


            return null!;
        }
    }
}
