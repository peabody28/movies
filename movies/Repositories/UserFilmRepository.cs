using Microsoft.EntityFrameworkCore;
using movies.Attributes;
using movies.Entities;
using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace movies.Repositories
{
    public class UserFilmRepository : IUserFilmRepository
    {
        [Dependency]
        public FilmDbContext FilmDbContext { get; set; }

        [Dependency]
        public IServiceProvider ServiceProvider { get; set; }

        public UserFilmRepository(DependencyFactory dependencyFactory)
        {
            dependencyFactory.ResolveDependency(this);
        }

        public IUserFilm Create(IUser user, IFilm film, ISection? section = null)
        {
            var userFilmEntity = ServiceProvider.GetRequiredService<IUserFilm>();
            userFilmEntity.Id = Guid.NewGuid();
            userFilmEntity.User = user;
            userFilmEntity.Film = film;
            userFilmEntity.Section = section;
                
            var userFilm = FilmDbContext.UserFilm.Add(userFilmEntity as UserFilmEntity);
            FilmDbContext.Entry(userFilmEntity.User).State = EntityState.Unchanged;
            FilmDbContext.Entry(userFilmEntity.Film).State = EntityState.Unchanged;
            if(userFilmEntity.Section != null)
                FilmDbContext.Entry(userFilmEntity.Section).State = EntityState.Unchanged;

            FilmDbContext.SaveChanges();

            return userFilm.Entity;
        }

        public IEnumerable<IUserFilm> Collection(IUser user, int pageSize, int pageNumber, out int count, ISection? section = null, bool isDeleted = false)
        {
            var collection = FilmDbContext.UserFilm
                .Include(uf => uf.Film)
                .Include(uf => uf.User)
                .Include(uf => uf.Film.Director)
                .Include(uf => uf.Film.Country)
                .Include(uf => uf.Section)
                .Where(uf => uf.User.Equals(user) && (section == null || uf.Section != null && uf.Section.Equals(section)) && uf.IsDeleted.Equals(isDeleted));

            count = collection.Count();

            return collection.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public IEnumerable<IUserFilm> Collection(IUser user, string text, int pageSize, int pageNumber, out int count, ISection? section = null, bool isDeleted = false)
        {
            var collection = FilmDbContext.UserFilm
                .Include(uf => uf.Film)
                .Include(uf => uf.User)
                .Include(uf => uf.Film.Director)
                .Include(uf => uf.Film.Country)
                .Include(uf => uf.Section)
                .Where(uf => uf.User.Equals(user) && (section == null || uf.Section != null && uf.Section.Equals(section)) && uf.IsDeleted.Equals(isDeleted) && EF.Functions.FreeText(uf.Film.Title, text));

            count = collection.Count();

            return collection.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public IUserFilm? Object(Guid id, bool isDeleted = false)
        {
            return FilmDbContext.UserFilm
                .Include(uf => uf.Film)
                .Include(uf => uf.User)
                .Include(uf => uf.Film.Director)
                .Include(uf => uf.Film.Country)
                .FirstOrDefault(uf => uf.Id.Equals(id) && uf.IsDeleted.Equals(isDeleted));
        }

        public IUserFilm? Object(IUser user, IFilm film, ISection? section = null, bool isDeleted = false)
        {
            return FilmDbContext.UserFilm.Include(uf => uf.Film).Include(uf => uf.User).Include(uf => uf.Film.Director).Include(uf => uf.Film.Country)
                .FirstOrDefault(uf => uf.User.Equals(user) && uf.Film.Equals(film) && (section == null || uf.Section != null && uf.Section.Equals(section)) && uf.IsDeleted.Equals(isDeleted));
        }

        public void Delete(IUserFilm userFilm)
        {
            userFilm.IsDeleted = true;

            FilmDbContext.Update(userFilm);

            FilmDbContext.SaveChanges();
        }
    }
}
