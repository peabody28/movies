using Microsoft.EntityFrameworkCore;
using movies.Entities;
using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;

namespace movies.Repositories
{
    public class UserFilmRepository : IUserFilmRepository
    {
        private FilmDbContext FilmDbContext { get; set; }

        private IServiceProvider ServiceProvider { get; set; }

        public UserFilmRepository(FilmDbContext filmDbContext, IServiceProvider serviceProvider)
        {
            FilmDbContext = filmDbContext;
            ServiceProvider = serviceProvider;
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

        public IEnumerable<IUserFilm> Collection(IUser user, ISection? section = null)
        {
            var userFilter = (IUserFilm uf) => uf.User.Equals(user);

            var sectionFilter = (IUserFilm uf) => section != null && uf.Section != null && uf.Section.Equals(section);

            return FilmDbContext.UserFilm
                .Include(uf => uf.Film)
                .Include(uf => uf.User)
                .Include(uf => uf.Film.Director)
                .Include(uf => uf.Film.Country)
                .Where(uf => userFilter(uf) && sectionFilter(uf)).ToList();
        }

        public IUserFilm? Object(IUser user, IFilm film, ISection? section = null)
        {
            var userFilter = (IUserFilm uf) => uf.User.Equals(user);

            var filmFilter = (IUserFilm uf) => uf.Film.Equals(film);

            var sectionFilter = (IUserFilm uf) => section != null && uf.Section != null && uf.Section.Equals(section);

            return FilmDbContext.UserFilm
                .Include(uf => uf.Film)
                .Include(uf => uf.User)
                .Include(uf => uf.Film.Director)
                .Include(uf => uf.Film.Country)
                .FirstOrDefault(uf => userFilter(uf) && filmFilter(uf) && sectionFilter(uf));
        }
    }
}
