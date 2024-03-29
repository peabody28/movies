﻿using Microsoft.EntityFrameworkCore;
using movies.Attributes;
using movies.Entities;
using System.Diagnostics;

namespace movies.Repositories
{
    public class FilmDbContext : DbContext
    {
        [Dependency]
        public IConfiguration Configuration { get; set; }

        public FilmDbContext(DependencyFactory dependencyFactory)
        {
            dependencyFactory.ResolveDependency(this);
        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<CountryEntity> Country { get; set; }
        public DbSet<DirectorEntity> Director { get; set; }
        public DbSet<RatingTypeEntity> RatingType { get; set; }
        public DbSet<RatingEntity> Rating { get; set; }
        public DbSet<FilmEntity> Film { get; set; }
        public DbSet<SectionEntity> Section { get; set; }
        public DbSet<UserFilmEntity> UserFilm { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Configuration.GetConnectionString("moviesDatabase");

            optionsBuilder.UseSqlServer(connString);
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
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

            modelBuilder.Entity<UserFilmEntity>()
                .HasOne(c => c.Film as FilmEntity);

            modelBuilder.Entity<UserFilmEntity>()
                .HasOne(c => c.User as UserEntity);

            modelBuilder.Entity<UserFilmEntity>()
                .HasOne(c => c.Section as SectionEntity);
        }
    }
}
