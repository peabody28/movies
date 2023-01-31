namespace movies.Interfaces.Entities
{
    public interface IDirector
    {
        Guid Id { get; set; }

        string FirstName { get; set; }

        string? LastName { get; set; }

        int? Age { get; set; }
    }
}
