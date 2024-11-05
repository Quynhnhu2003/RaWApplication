namespace RaWMVC.Data.Entities
{
    public class ChapterReadCount
    {
        public Guid ChapterReadId { get; set; }
        public Guid ChapterId { get; set; }
        public string UserId { get; set; }
        public DateTime ReadDate { get; set; }
    }
}
