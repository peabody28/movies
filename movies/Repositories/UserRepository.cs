using movies.Attributes;
using movies.Entities;
using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;

namespace movies.Repositories
{
    public class UserRepository : IUserRepository
    {
        [Dependency]
        public FilmDbContext FilmDbContext { get; set; }

        [Dependency]
        public IServiceProvider ServiceProvider { get; set; }

        public UserRepository(DependencyFactory dependencyFactory)
        {
            dependencyFactory.ResolveDependency(this);
        }

        public IUser Create(string nickName, string email, string passwordHash)
        {
            try
            {
                var userEntity = ServiceProvider.GetRequiredService<IUser>();
                userEntity.Id = Guid.NewGuid();
                userEntity.NickName = nickName;
                userEntity.Email = email;
                userEntity.PasswordHash = passwordHash;

                var user = FilmDbContext.User.Add(userEntity as UserEntity);

                FilmDbContext.SaveChanges();

                return user.Entity;
            }
            catch
            {
                return null!;
            }
        }


        public IUser? Object(string nickName, string passwordHash)
        {
            return FilmDbContext.User.FirstOrDefault(c => c.NickName.Equals(nickName) && c.PasswordHash.Equals(passwordHash));
        }

        public IUser? Object(string nickName)
        {
            return FilmDbContext.User.FirstOrDefault(c => c.NickName.Equals(nickName));
        }
    }
}
