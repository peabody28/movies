namespace movies.Interfaces.Entities
{
    public interface IDirector
    {
        Guid Id { get; set; }

        string Name { get; set; }

        int? Age { get; set; }
    }
}
