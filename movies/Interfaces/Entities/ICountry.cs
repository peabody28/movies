namespace movies.Interfaces.Entities
{
    public interface ICountry
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Code { get; set; }
    }
}
