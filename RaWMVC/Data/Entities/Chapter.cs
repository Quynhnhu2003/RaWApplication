namespace RaWMVC.Data.Entities
{
    public class Chapter
    {
        public Chapter()
        {
            ChapterId = Guid.NewGuid();
        }
        public Guid ChapterId { get; set; }
        public string ChapterTitle { get; set; }
        public int Position { get; set; }
        public string ChapterContent { get; set; }
        public DateTime PublishDate { get; set; }
        public Boolean IsPublished { get; set; }
        public Boolean IsDraft { get; set; }
        public int ReadCount { get; set; }

        //=== Navigational Property ===//
        public Guid StoryId { get; set; }
        public virtual Story Story { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
