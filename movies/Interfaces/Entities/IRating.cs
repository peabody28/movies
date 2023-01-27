namespace movies.Interfaces.Entities
{
    public interface IRating
    {
        public Guid Id { get; set; }

        public IFilm Film { get; set; }

        public Guid FilmFk { get; set; }

        public IRatingType RatingType { get; set; }

        public Guid RatingTypeFk { get; set; }

        public decimal Value { get; set; }
    }
}
