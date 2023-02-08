using movies.Interfaces.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace movies.Entities
{
    public class UserFilmEntity : IUserFilm
    {
        public Guid Id { get; set; }

        [ForeignKey("Film")]
        public Guid FilmFk { get; set; }
        public IFilm Film { get; set; }

        [ForeignKey("User")]
        public Guid UserFk { get; set; }
        public IUser User { get; set; }

        [ForeignKey("Section")]
        public Guid? SectionFk { get; set; }
        public ISection? Section { get; set; }

        public bool IsDeleted { get; set; }
    }
}
