namespace movies.Interfaces.Entities
{
    public interface IFilm
    {
        Guid Id { get; set; }

        string Title { get; set; }

        string Description { get; set; }

        IDirector Director { get; set; }

        Guid DirectorFk { get; set; }

        ICountry Country { get; set; }

        Guid CountryFk { get; set; }    

        int Year { get; set; }
    }
}
