using movies.Interfaces.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace movies.Entities
{
    public class RatingEntity : IRating
    {
        public Guid Id { get; set; }

        [ForeignKey("Film")]
        public Guid FilmFk { get; set; }
        public IFilm Film { get; set; }

       
        [ForeignKey("RatingType")]
        public Guid RatingTypeFk { get; set; }
        public IRatingType RatingType { get; set; }

        public decimal Value { get; set; }
    }
}
