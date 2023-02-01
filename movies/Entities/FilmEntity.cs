using movies.Interfaces.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace movies.Entities
{
    public class FilmEntity : IFilm
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

       
        public IDirector Director { get; set; }
        [ForeignKey("Director")]
        public Guid DirectorFk { get; set; }

      
        public ICountry Country { get; set; }
        [ForeignKey("Country")]
        public Guid CountryFk { get; set; }

        public int? Year { get; set; }

        public bool IsDeleted { get; set; }
    }
}
