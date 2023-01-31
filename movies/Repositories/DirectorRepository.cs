using movies.Entities;
using movies.Interfaces.Entities;
using movies.Interfaces.Repositories;

namespace movies.Repositories
{
    public class DirectorRepository : IDirectorRepository
    {
        private FilmDbContext FilmDbContext { get; set; }

        private IServiceProvider ServiceProvider { get; set; }

        public DirectorRepository(FilmDbContext filmDbContext, IServiceProvider serviceProvider)
        {
            FilmDbContext = filmDbContext;
            ServiceProvider = serviceProvider;
        }

        public IDirector Create(string firstName, string? lastName = null, int? age = null)
        {
            var directorEntity = ServiceProvider.GetRequiredService<IDirector>();
            directorEntity.Id = Guid.NewGuid();
            directorEntity.FirstName = firstName;
            directorEntity.LastName = lastName;
            directorEntity.Age = age;

            var director = FilmDbContext.Director.Add(directorEntity as DirectorEntity);
           
            FilmDbContext.SaveChanges();

            return director.Entity;
        }

        public IDirector Object(string firstName, string lastName)
        {
            return FilmDbContext.Director.FirstOrDefault(d => d.FirstName.Equals(firstName) && d.LastName.Equals(lastName));
        }
    }
}
