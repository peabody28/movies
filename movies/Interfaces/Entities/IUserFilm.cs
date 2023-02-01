namespace movies.Interfaces.Entities
{
    public interface IUserFilm
    {
        Guid Id { get; set; }

        Guid FilmFk { get; set; }

        IFilm Film { get; set; }

        Guid UserFk { get; set; }

        IUser User { get; set; }

        Guid? SectionFk { get; set; }

        ISection? Section { get; set; }
    }
}
