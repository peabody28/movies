using Microsoft.EntityFrameworkCore;
using movies.Entities;
using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;

namespace movies.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private FilmDbContext FilmDbContext { get; set; }

        private IServiceProvider ServiceProvider { get; set; }

        public FilmRepository(FilmDbContext filmDbContext, IServiceProvider serviceProvider)
        {
            FilmDbContext = filmDbContext;
            ServiceProvider = serviceProvider;
        }

        public IFilm? Object(Guid id)
        {
            return FilmDbContext.Film.FirstOrDefault(film => film.Id.Equals(id));
        }

        public IFilm? Object(string title)
        {
            return FilmDbContext.Film
                .Include(c => c.Country)
                .Include(c => c.Director)
                .FirstOrDefault(f => !string.IsNullOrEmpty(f.Title) ? f.Title.Equals(title) : false);
        }

        public void Update(IFilm film)
        {
            FilmDbContext.Film.Update(film as FilmEntity);
            FilmDbContext.SaveChanges();
        }

        public IEnumerable<IFilm> Collection()
        {
            return FilmDbContext.Film.Include(c => c.Country).Include(c => c.Director).ToList();
        }

        public IFilm? Create(IDirector director, IRatingType ratingType, decimal ratingValue, ICountry country, string title, string? description, int? year = null)
        {
            var transactionContext = FilmDbContext.Database.BeginTransaction();

            try
            {
                var filmEntity = ServiceProvider.GetRequiredService<IFilm>();
                filmEntity.Id = Guid.NewGuid();
                filmEntity.Title = title;
                filmEntity.Description = description;
                filmEntity.Director = director;
                filmEntity.Country = country;
                filmEntity.Year = year;

                var film = FilmDbContext.Film.Add(filmEntity as FilmEntity);
                FilmDbContext.Entry(filmEntity.Country).State = EntityState.Unchanged;
                FilmDbContext.Entry(filmEntity.Director).State = EntityState.Unchanged;

                FilmDbContext.SaveChanges();


                var ratingEntity = ServiceProvider.GetRequiredService<IRating>();
                ratingEntity.Id = Guid.NewGuid();
                ratingEntity.Film = filmEntity;
                ratingEntity.RatingType = ratingType;
                ratingEntity.Value = ratingValue;

                FilmDbContext.Rating.Add(ratingEntity as RatingEntity);
                FilmDbContext.Entry(ratingEntity.RatingType).State = EntityState.Unchanged;
                FilmDbContext.Entry(ratingEntity.Film).State = EntityState.Unchanged;

                FilmDbContext.SaveChanges();

                transactionContext.Commit();

                return film.Entity;
            }
            catch
            {
                return null;
            }
        }
    }
}
