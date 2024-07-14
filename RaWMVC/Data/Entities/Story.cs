namespace RaWMVC.Data.Entities
{
    public class Story
    {
        public Guid storyId { get; set; }
        public string storyTitle { get; set; }
        public string storyDescription { get; set; }
        public string? coverImage { get; set; }
        public DateTime publishDate { get; set; }
        public int Position { get; set; }

        // === Navigational Property === //
        public Guid genreId { get; set; }
        public virtual Genre Genre { get; set; }
        public Guid statusId { get; set; }
        public virtual Status Status { get; set; }
        public Guid tagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
