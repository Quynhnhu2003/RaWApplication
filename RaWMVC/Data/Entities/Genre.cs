namespace RaWMVC.Data.Entities
{
    public class Genre
    {
        public Genre()
        {
            genreId = Guid.NewGuid();
        }
        public Guid genreId { get; set; }
        public string genreName { get; set; }
        public string? genreDescription { get; set; }
        public int Position { get; set; }
    }
}
