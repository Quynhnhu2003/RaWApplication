using RaWMVC.Areas.Identity.Data;

namespace RaWMVC.Data.Entities
{
    public class Library
    {
        public string Id { get; set; }
        public bool InMyLibrary { get; set; }
        public bool IsInReadingList { get; set; }
        public Guid? StoryId { get; set; }
        public virtual Story Story { get; set; }
        public string UserId { get; set; }
        public Guid ReadingListsId { get; set; }
        public ReadingList ReadingLists { get; set; }
    }
}
