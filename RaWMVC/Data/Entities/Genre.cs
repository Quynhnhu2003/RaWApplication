namespace RaWMVC.Data.Entities
{
    public class Genre
    {
        public Genre()
        {
            GenreId = Guid.NewGuid();
        }
        public Guid GenreId { get; set; }
        public string GenreName { get; set; }
        public string? GenreDescription { get; set; }
        public int Position { get; set; }
    }
}
