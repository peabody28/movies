using Microsoft.EntityFrameworkCore;
using movies.Entities;

namespace movies.Repositories
{
    public class FilmDbContext : DbContext
    {
        private IConfiguration Configuration { get; set; }

        public FilmDbContext(IConfiguration config)
        {
            Configuration = config;
        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<CountryEntity> Country { get; set; }
        public DbSet<DirectorEntity> Director { get; set; }
        public DbSet<RatingTypeEntity> RatingType { get; set; }
        public DbSet<RatingEntity> Rating { get; set; }
        public DbSet<FilmEntity> Film { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Configuration.GetConnectionString("moviesDatabase");

            optionsBuilder.UseSqlServer(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RatingEntity>()
                .HasOne(c => c.RatingType as RatingTypeEntity);

            modelBuilder.Entity<RatingEntity>()
                .HasOne(c => c.Film as FilmEntity);

            modelBuilder.Entity<FilmEntity>()
                .HasOne(c => c.Director as DirectorEntity);

            modelBuilder.Entity<FilmEntity>()
               .HasOne(c => c.Country as CountryEntity);
        }
    }
}
