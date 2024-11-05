using RaWMVC.Areas.Identity.Data;

namespace RaWMVC.Data.Entities
{
    public class ReadingList
    {
        public ReadingList()
        {
            ReadingListStories = new List<ReadingListStory>();
        }
        public Guid ReadingListsId { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public ICollection<ReadingListStory> ReadingListStories { get; set; }
    }
}
